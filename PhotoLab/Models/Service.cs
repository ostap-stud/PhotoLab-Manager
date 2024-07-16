using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace PhotoLab.Models
{
    public class Service
    {
        [Key]   // Встановлення атрибута як Primary Key
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]   // Auto-Incremented
        public int ServiceId { get; set; }

        [DisplayName("Послуга")]
        [MinLength(2, ErrorMessage = "Мінімум 2 символи")]
        [MaxLength(50, ErrorMessage = "Перевищено ліміт у 50 символів")]
        [Required(ErrorMessage = "Обов'язкове поле")]
        public string? ServiceName { get; set; }

        [DisplayName("Опис")]
        [DataType(DataType.MultilineText)]
        public string? Description { get; set; }

        [DisplayName("Ціна")]
        [RegularExpression(@"\d+(\.\d{1,2})?", ErrorMessage = "Некоректне значення")]
        [Required(ErrorMessage = "Обов'язкове поле")]
        public decimal Price { get; set; }

        [DisplayName("URI Картинки")]
        public string? ImageURI { get; set; }

        public virtual ICollection<Order>? Orders { get; set; }
    }
}
