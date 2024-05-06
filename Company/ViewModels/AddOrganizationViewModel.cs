using System;
using System.ComponentModel.DataAnnotations;
namespace Company.ViewModels
{
    public class AddOrganizationViewModel
    {
        [Display(Name = "Код организации")]
        public int Код_организации { get; set; }

        [Display(Name = "Код статуса")]
        public int Код_статуса { get; set; }

        [Display(Name = "Название организации")]
        [Required(ErrorMessage = "Поле 'Название организации' обязательно для заполнения")]
        public string Название_организации { get; set; }

        [Display(Name = "ИНН")]
        [Required(ErrorMessage = "Поле 'ИНН' обязательно для заполнения")]
        public string ИНН { get; set; }

        [Display(Name = "КПП")]
        public string КПП { get; set; }

        [Display(Name = "ОГРН")]
        public string ОГРН { get; set; }

        [Display(Name = "Код")]
        public int Код { get; set; }

        [Display(Name = "Тип организации")]
        public string Тип_организации { get; set; }

        [Display(Name = "Дата закрытия")]
        public DateTime Дата_закрытия { get; set; }

        [Display(Name = "Код родительской организации")]
        public int Код_родительской_организации { get; set; }
    }
}
