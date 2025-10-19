using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestEntityFrameworkProject.Models
{
    /// <summary>
    /// Модель продукта для системы управления товарами
    /// </summary>
    public class Product
    {
        /// <summary>
        /// Уникальный идентификатор продукта
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Название продукта
        /// </summary>
        [Required(ErrorMessage = "Введите название продукта")]
        [StringLength(200, MinimumLength = 2, ErrorMessage = "Название должно быть от 2 до 200 символов")]
        [Display(Name = "Название")]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Описание продукта
        /// </summary>
        [Required(ErrorMessage = "Введите описание продукта")]
        [StringLength(1000, MinimumLength = 5, ErrorMessage = "Описание должно быть от 5 до 1000 символов")]
        [Display(Name = "Описание")]
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Цена продукта
        /// </summary>
        [Required(ErrorMessage = "Введите цену продукта")]
        [Range(0.01, 999999.99, ErrorMessage = "Цена должна быть от 0.01 до 999999.99")]
        [Column(TypeName = "decimal(18,2)")]
        [Display(Name = "Цена")]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        /// <summary>
        /// Дата и время создания продукта (UTC)
        /// </summary>
        [Display(Name = "Дата создания")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
