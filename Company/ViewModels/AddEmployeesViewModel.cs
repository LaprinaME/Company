using System;
using System.ComponentModel.DataAnnotations;

namespace Company.ViewModels
{
    public class AddEmployeesViewModel
    {
        [Required(ErrorMessage = "Введите ФИО")]
        public string ФИО { get; set; }

        [Required(ErrorMessage = "Введите пол")]
        public string Пол { get; set; }

        [Required(ErrorMessage = "Введите дату рождения")]
        [DataType(DataType.Date)]
        [Display(Name = "Дата рождения")]
        public DateTime Дата_рождения { get; set; }

        [Required(ErrorMessage = "Введите СНИЛС")]
        [RegularExpression(@"^\d{3}-\d{3}-\d{3}\s?\d{2}$", ErrorMessage = "Некорректный формат СНИЛС")]
        public string СНИЛС { get; set; }

        [Required(ErrorMessage = "Введите мобильный телефон")]
        [RegularExpression(@"^\+?(\d[\d-. ]+)?(\([\d-. ]+\))?[\d-. ]+\d$", ErrorMessage = "Некорректный формат телефона")]
        [Display(Name = "Мобильный телефон")]
        public string Мобильный_телефон { get; set; }

        [Required(ErrorMessage = "Введите адрес электронной почты")]
        [EmailAddress(ErrorMessage = "Некорректный адрес электронной почты")]
        [Display(Name = "Адрес электронной почты")]
        public string Адрес_электронной_почты { get; set; }

        [Required(ErrorMessage = "Введите адрес проживания")]
        [Display(Name = "Адрес проживания")]
        public string Адрес_проживания { get; set; }

        [Required(ErrorMessage = "Введите должность")]
        [Display(Name = "Должность")]
        public string Должность { get; set; }

        // Дополнительные поля для кода аккаунта, кода статуса и кода сотрудника
        public int Код_аккаунта { get; set; }
        public int Код_статуса { get; set; }
        public int Код_сотрудника { get; set; }
    }
}
