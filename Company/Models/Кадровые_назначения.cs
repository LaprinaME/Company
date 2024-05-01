using System;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Company.Models
{
    public class Кадровые_назначения
    {
        [Key] public int Код_назначения { get; set; }
        public int Код_сотрудника { get; set; }
        public int Код_организации { get; set; }
        public int Код_периода { get; set; }
        public int Код_отдела { get; set; }
        public string Должность { get; set; }
        public string Вид_занятости { get; set; }
        public string ФИО_руководителя { get; set; }
        public int Табельный_номер { get; set; }
        public string Номер_кабинета { get; set; }
        public string Рабочий_телефон { get; set; }
        public string Рабочий_адрес_электронной_почты { get; set; }

        [ForeignKey("Код_отдела")]
        public Отделы Отделы { get; set; }

        [ForeignKey("Код_сотрудника")]
        public Сотрудники Сотрудники { get; set; }

        [ForeignKey("Код_организации")]
        public Организации Организации { get; set; }

        [ForeignKey("Код_периода")]
        public Периоды_неисполнения_обязанностей Периоды_неисполнения_обязанностей { get; set; }
    }
}
