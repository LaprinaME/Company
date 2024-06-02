using System;
using System.ComponentModel.DataAnnotations;

namespace Company.ViewModels
{
    public class EditEmployeesViewModel
    {
        [Key]
        public int Код_сотрудника { get; set; }

        [Required(ErrorMessage = "Код статуса обязателен")]
        public int Код_статуса { get; set; }

        [Required(ErrorMessage = "Код аккаунта обязателен")]
        public int Код_аккаунта { get; set; }

        [Required(ErrorMessage = "ФИО обязательно")]
        [StringLength(100, ErrorMessage = "ФИО не должно превышать 100 символов")]
        public string ФИО { get; set; }

        [Required(ErrorMessage = "Пол обязателен")]
        public string Пол { get; set; }

        [Required(ErrorMessage = "Дата рождения обязательна")]
        [DataType(DataType.Date)]
        public DateTime Дата_рождения { get; set; }

        [Required(ErrorMessage = "СНИЛС обязателен")]
        [RegularExpression(@"\d{3}-\d{3}-\d{3} \d{2}", ErrorMessage = "СНИЛС должен быть в формате XXX-XXX-XXX XX")]
        public string СНИЛС { get; set; }

        [Required(ErrorMessage = "Мобильный телефон обязателен")]
        [Phone(ErrorMessage = "Неправильный формат мобильного телефона")]
        public string Мобильный_телефон { get; set; }

        [Required(ErrorMessage = "Адрес электронной почты обязателен")]
        [EmailAddress(ErrorMessage = "Неправильный формат адреса электронной почты")]
        public string Адрес_электронной_почты { get; set; }

        [Required(ErrorMessage = "Адрес проживания обязателен")]
        [StringLength(200, ErrorMessage = "Адрес проживания не должен превышать 200 символов")]
        public string Адрес_проживания { get; set; }

        [Required(ErrorMessage = "Должность обязательна")]
        [StringLength(50, ErrorMessage = "Должность не должна превышать 50 символов")]
        public string Должность { get; set; }
    }
}
