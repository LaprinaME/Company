using System;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Company.Models
{
    public class Сотрудники
    {
        [Key] public int Код_сотрудника { get; set; }
        public int Код_статуса { get; set; }
        public int Код_аккаунта { get; set; }
        public string ФИО { get; set; }
        public string Пол { get; set; }
        public DateTime Дата_рождения { get; set; }
        public string СНИЛС { get; set; }
        public string Мобильный_телефон { get; set; }
        public string Адрес_электронной_почты { get; set; }
        public string Адрес_проживания { get; set; }
        public string Должность { get; set; }

        [ForeignKey("Код_статуса")]
        public Статусы Статусы { get; set; }
        [ForeignKey("Код_аккаунта")]
        public Аккаунты Аккаунты { get; set; }
    }
}

