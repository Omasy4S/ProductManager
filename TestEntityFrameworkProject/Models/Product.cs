using System.ComponentModel.DataAnnotations;

namespace TestEntityFrameworkProject.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Введите Имя продукта")]
        public string Name { get; set; } = string.Empty;
        [Required(ErrorMessage = "Введите описание продукта")]
        public string Description { get; set; } = string.Empty;
        [Required(ErrorMessage = "Введите цену продукта")]
        public decimal? Price { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
    public class ProductStatsViewModel
    {
        public int TotalCount { get; set; }
        public decimal AveragePrice { get; set; }
        public string MostExpensiveName { get; set; } = "—";
        public decimal MostExpensivePrice { get; set; }
        public string CheapestName { get; set; } = "—";
        public decimal CheapestPrice { get; set; }
        public string LatestProductName { get; set; } = "—";
        public DateTime LatestProductDate { get; set; }
    }
}
