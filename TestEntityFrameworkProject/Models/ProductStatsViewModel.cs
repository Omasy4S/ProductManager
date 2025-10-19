namespace TestEntityFrameworkProject.Models
{
    /// <summary>
    /// Модель представления статистики по продуктам
    /// </summary>
    public class ProductStatsViewModel
    {
        /// <summary>
        /// Общее количество продуктов
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// Средняя цена продуктов
        /// </summary>
        public decimal AveragePrice { get; set; }

        /// <summary>
        /// Название самого дорогого продукта
        /// </summary>
        public string MostExpensiveName { get; set; } = "—";

        /// <summary>
        /// Цена самого дорогого продукта
        /// </summary>
        public decimal MostExpensivePrice { get; set; }

        /// <summary>
        /// Название самого дешевого продукта
        /// </summary>
        public string CheapestName { get; set; } = "—";

        /// <summary>
        /// Цена самого дешевого продукта
        /// </summary>
        public decimal CheapestPrice { get; set; }

        /// <summary>
        /// Название последнего добавленного продукта
        /// </summary>
        public string LatestProductName { get; set; } = "—";

        /// <summary>
        /// Дата создания последнего продукта
        /// </summary>
        public DateTime LatestProductDate { get; set; }
    }
}
