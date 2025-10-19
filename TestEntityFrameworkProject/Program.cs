using Microsoft.EntityFrameworkCore;
using TestEntityFrameworkProject.Data;

// Создание builder-а приложения
var builder = WebApplication.CreateBuilder(args);

// ========================================
// Регистрация сервисов
// ========================================

// Добавление поддержки контроллеров и представлений MVC
builder.Services.AddControllersWithViews();

// Регистрация контекста базы данных с SQLite
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// ========================================
// Построение приложения
// ========================================
var app = builder.Build();

// ========================================
// Инициализация базы данных при первом запуске
// ========================================
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<AppDbContext>();
        
        // Применяем миграции автоматически
        // Для production рекомендуется использовать явные миграции через CLI
        context.Database.EnsureCreated();
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Произошла ошибка при создании/миграции базы данных.");
    }
}

// ========================================
// Конфигурация HTTP request pipeline
// ========================================

// Обработка ошибок в зависимости от окружения
if (!app.Environment.IsDevelopment())
{
    // В production используем общую страницу ошибок
    app.UseExceptionHandler("/Home/Error");
    
    // Включаем HSTS для безопасности
    app.UseHsts();
}

// Перенаправление HTTP на HTTPS
app.UseHttpsRedirection();

// Поддержка статических файлов (CSS, JS, изображения)
app.UseStaticFiles();

// Включение маршрутизации
app.UseRouting();

// Включение авторизации
app.UseAuthorization();

// Настройка маршрута по умолчанию для контроллеров
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// ========================================
// Запуск приложения
// ========================================
app.Run();
