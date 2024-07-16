using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace PhotoLab.Models
{
    public class Order
    {
        [Key]   // Встановлення атрибута як Primary Key
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]   // Auto-Incremented
        public int OrderId { get; set; }

        [Display(Name = "Ім'я клієнта")]
        [MinLength(2, ErrorMessage = "Мінімум 2 символи")]  // Мінімальна довжина
        [MaxLength(50, ErrorMessage = "Перевищено ліміт у 50 символів")] // Максимальна довжина
        [Required(ErrorMessage = "Обов'язкове поле")]  // Обов'язковий
        public string? Client_Name { get; set; }

        [Display(Name = "Прізвище клієнта")]
        [MinLength(2, ErrorMessage = "Мінімум 2 символи")]
        [MaxLength(50, ErrorMessage = "Перевищено ліміт у 50 символів")]
        [Required(ErrorMessage = "Обов'язкове поле")]
        public string? Client_Surname { get; set; }

        [Display(Name = "Статус")]
        [Required(ErrorMessage = "Обов'язкове поле")]
        public Status Status { get; set; }

        private DateTime _Date = TrimSeconds(DateTime.Now);
        [Display(Name = "Дата")]
        public DateTime? Date 
        { 
            get 
            {
                return _Date; 
            } 
            set 
            { 
                if (value != null)
                {
                    _Date = TrimSeconds(value.Value);
                }                
            } 
        }

        [Display(Name = "Працівник")]
        [MinLength(2, ErrorMessage = "Мінімум 2 символи")]
        [MaxLength(50, ErrorMessage = "Перевищено ліміт у 50 символів")]
        public string? Worker { get; set; }
        public int ServiceId { get; set; }
        public virtual Service? Service { get; set; }

        [Display(Name = "Кількість")]
        [RegularExpression(@"\d+", ErrorMessage = "Некоректне значення")]
        [Required(ErrorMessage = "Обов'язкове поле")]
        public int Count { get; set; }

        [Display(Name = "Загалом")]
        public decimal? Total { get; set; } = decimal.Zero;

        public static DateTime TrimSeconds(DateTime datetime)
        {
            return new DateTime(datetime.Year, datetime.Month, datetime.Day, datetime.Hour, datetime.Minute, 0, datetime.Kind);
        }

    }

    public enum Status
    {
        [Description("Замовлення виконується")]
        [Display(Name = "У процесі")]
        Processing,

        [Description("Замовлення було успішно виконане")]
        [Display(Name = "Успішно")]
        Success,

        [Description("Замовлення було скасоване")]
        [Display(Name = "Скасовано")]
        Cancelled
    }
}
