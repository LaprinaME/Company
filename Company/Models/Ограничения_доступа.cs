using System;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Company.Models
{
    public class Ограничения_доступа
    {
        [Key] public int Код_ограничения { get; set; }
        public int Код_сотрудника { get; set; }
        public int Код_расписания { get; set; }
        public DateTime Период_доступа_с { get; set; }
        public DateTime Период_доступа_по { get; set; }
        public string Доступ_по_IP { get; set; }

        [ForeignKey("Код_сотрудника")]
        public Сотрудники Сотрудники { get; set; }

        [ForeignKey("Код_расписания")]
        public Расписание Расписание { get; set; }
    }
}

