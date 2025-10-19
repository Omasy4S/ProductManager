using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestEntityFrameworkProject.Data;
using TestEntityFrameworkProject.Models;

namespace TestEntityFrameworkProject.Controllers
{
    /// <summary>
    /// Контроллер для управления продуктами
    /// </summary>
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        /// <summary>
        /// Конструктор с внедрением зависимости контекста базы данных
        /// </summary>
        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Главная страница со списком продуктов, поиском и сортировкой
        /// </summary>
        /// <param name="searchString">Строка поиска по названию или описанию</param>
        /// <param name="sort">Параметр сортировки</param>
        public async Task<IActionResult> Index(string searchString, string sort)
        {
            // Начинаем с IQueryable для отложенного выполнения запроса
            IQueryable<Product> query = _context.Products;

            // Применяем фильтр поиска на уровне БД
            if (!string.IsNullOrEmpty(searchString))
            {
                query = query.Where(p => EF.Functions.Like(p.Name, $"%{searchString}%") ||
                                        EF.Functions.Like(p.Description, $"%{searchString}%"));
                ViewData["CurrentFilter"] = searchString;
            }
            
            // Применяем сортировку на уровне БД (для совместимости с SQLite decimal конвертируем в double)
            ViewBag.CurrentSort = sort;
            query = sort switch
            {
                "name_desc" => query.OrderByDescending(p => p.Name),
                "price_asc" => query.OrderBy(p => (double)p.Price),
                "price_desc" => query.OrderByDescending(p => (double)p.Price),
                "date_asc" => query.OrderBy(p => p.CreatedAt),
                "date_desc" => query.OrderByDescending(p => p.CreatedAt),
                _ => query.OrderBy(p => p.Name) // По умолчанию сортируем по имени (A-Z)
            };

            // Выполняем запрос асинхронно один раз
            var products = await query.ToListAsync();

            // Вычисляем статистику оптимизированным способом
            var stats = new ProductStatsViewModel();
            
            if (products.Any())
            {
                stats.TotalCount = products.Count;
                stats.AveragePrice = products.Average(p => p.Price);
                
                var orderedByPrice = products.OrderBy(p => p.Price).ToList();
                var cheapest = orderedByPrice.First();
                var mostExpensive = orderedByPrice.Last();
                
                stats.CheapestName = cheapest.Name;
                stats.CheapestPrice = cheapest.Price;
                stats.MostExpensiveName = mostExpensive.Name;
                stats.MostExpensivePrice = mostExpensive.Price;
                
                var latest = products.OrderByDescending(p => p.CreatedAt).First();
                stats.LatestProductName = latest.Name;
                stats.LatestProductDate = latest.CreatedAt;
            }

            ViewBag.Stats = stats;
            return View(products);
        }

        /// <summary>
        /// Отображение формы создания нового продукта
        /// </summary>
        [HttpGet]
        public IActionResult Create() => View();

        /// <summary>
        /// Создание нового продукта
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Products.Add(product);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "✅ Продукт успешно создан!";
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        /// <summary>
        /// Отображение формы редактирования продукта
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        /// <summary>
        /// Сохранение изменений продукта
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Product product)
        {
            if (id != product.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "✅ Продукт успешно обновлён!";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await ProductExists(product.Id))
                    {
                        return NotFound();
                    }
                    ModelState.AddModelError("", "Произошла ошибка конкурентности. Попробуйте снова.");
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "Произошла ошибка при сохранении.");
                }
            }

            return View(product);
        }

        /// <summary>
        /// Удаление продукта
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "✅ Продукт успешно удалён!";
            }
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Проверка существования продукта
        /// </summary>
        private async Task<bool> ProductExists(int id)
        {
            return await _context.Products.AnyAsync(e => e.Id == id);
        }
    }
}
