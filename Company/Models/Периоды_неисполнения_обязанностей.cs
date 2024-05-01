using System;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Company.Models
{
    public class Периоды_неисполнения_обязанностей
    {
        [Key] public int Код_периода { get; set; }
        public int Код_сотрудника { get; set; }
        public DateTime Дата_начала { get; set; }
        public DateTime Дата_окончания { get; set; }
        public DateTime Дата_увольнения { get; set; }
        public DateTime Дата_приема { get; set; }
        public string Тип { get; set; }
        public string Причина_отсутствия_на_работе { get; set; }
        public string Причина_увольнения { get; set; }

        [ForeignKey("Код_сотрудника")]
        public Сотрудники Сотрудники { get; set; }
    }
}
