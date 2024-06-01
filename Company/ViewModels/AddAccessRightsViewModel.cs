using System;
using System.ComponentModel.DataAnnotations;

namespace Company.ViewModels
{
    public class AddAccessRightsViewModel
    {
        [Required]
        [Display(Name = "Код права")]
        public int Код_права { get; set; }

        [Required]
        [Display(Name = "Код роли")]
        public int Код_роли { get; set; }

        [Required]
        [Display(Name = "Код приложения")]
        public int Код_приложения { get; set; }

        [Required]
        [Display(Name = "Название права")]
        public string Название_права { get; set; }

        [Required]
        [Display(Name = "Начало действия доступа")]
        [DataType(DataType.Date)]
        public DateTime Начало_действия_доступа { get; set; }

        [Required]
        [Display(Name = "Окончание действия доступа")]
        [DataType(DataType.Date)]
        public DateTime Окончание_действия_доступа { get; set; }

        [Required]
        [Display(Name = "Тип доступа")]
        public string Тип_доступа { get; set; }
    }
}
