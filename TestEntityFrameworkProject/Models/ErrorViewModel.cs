namespace TestEntityFrameworkProject.Models
{
    /// <summary>
    /// Модель представления ошибок
    /// </summary>
    public class ErrorViewModel
    {
        /// <summary>
        /// Идентификатор запроса для отслеживания ошибок
        /// </summary>
        public string? RequestId { get; set; }

        /// <summary>
        /// Показывать ли идентификатор запроса
        /// </summary>
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
