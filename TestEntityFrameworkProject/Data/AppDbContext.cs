using Microsoft.EntityFrameworkCore;
using TestEntityFrameworkProject.Models;

namespace TestEntityFrameworkProject.Data
{
    /// <summary>
    /// Контекст базы данных приложения для работы с продуктами
    /// </summary>
    public class AppDbContext : DbContext
    {
        /// <summary>
        /// Конструктор контекста базы данных
        /// </summary>
        /// <param name="options">Опции конфигурации контекста</param>
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        /// <summary>
        /// Набор данных продуктов
        /// </summary>
        public DbSet<Product> Products => Set<Product>();

        /// <summary>
        /// Конфигурация модели базы данных
        /// </summary>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Конфигурация сущности Product
            modelBuilder.Entity<Product>(entity =>
            {
                // Создаем индекс на поле Name для ускорения поиска
                entity.HasIndex(p => p.Name)
                    .HasDatabaseName("IX_Product_Name");

                // Создаем индекс на поле Price для ускорения сортировки
                entity.HasIndex(p => p.Price)
                    .HasDatabaseName("IX_Product_Price");

                // Создаем индекс на поле CreatedAt для ускорения сортировки по дате
                entity.HasIndex(p => p.CreatedAt)
                    .HasDatabaseName("IX_Product_CreatedAt");

                // Устанавливаем значение по умолчанию для CreatedAt
                entity.Property(p => p.CreatedAt)
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");
            });
        }
    }
}
