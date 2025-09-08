using Microsoft.AspNetCore.Mvc;
using TestEntityFrameworkProject.Data;
using TestEntityFrameworkProject.Models;

namespace TestEntityFrameworkProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext context;

        public HomeController(AppDbContext Сontext)
        {
            context = Сontext;
        }

        public IActionResult Index(string searchString)
        {
            var query = context.Products.ToList();

            if (!string.IsNullOrEmpty(searchString))
            {
                // Ищем по названию ИЛИ по описанию если товаров <1000, если их больше нужно использовать более производительные способы
                query = query.Where(p => p.Name.IndexOf(searchString, StringComparison.OrdinalIgnoreCase) >= 0 ||
                                         p.Description.IndexOf(searchString, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
                ViewData["CurrentFilter"] = searchString; // Передаём значение обратно в View
            }

            var products = query.ToList();

            // 📊 Вычисляем статистику
            var stats = new ProductStatsViewModel
            {
                TotalCount = products.Count,
                AveragePrice = (decimal)(products.Any() ? products.Average(p => p.Price) : 0),
                LatestProductDate = products.Any() ? products.Max(p => p.CreatedAt) : DateTime.MinValue
            };

            if (products.Any())
            {
                var mostExpensive = products.OrderByDescending(p => p.Price).First();
                stats.MostExpensiveName = mostExpensive.Name;
                stats.MostExpensivePrice = (decimal)mostExpensive.Price;

                var cheapest = products.OrderBy(p => p.Price).First();
                stats.CheapestName = cheapest.Name;
                stats.CheapestPrice = (decimal)cheapest.Price;

                var latest = products.OrderByDescending(p => p.CreatedAt).First();
                stats.LatestProductName = latest.Name;
            }

            // Передаём статистику в View через ViewBag
            ViewBag.Stats = stats;

            return View(products);
        }

        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                context.Products.Add(product);
                context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = context.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Product product)
        {
            if (id != product.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    context.Update(product);
                    context.SaveChanges();
                    TempData["SuccessMessage"] = "✅ Товар успешно обновлён!";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "Произошла ошибка при сохранении.");
                }
            }

            return View(product);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var product = context.Products.Find(id);
            if (product != null)
            {
                context.Products.Remove(product);
                context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
