using System;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Company.Models
{
    public class Расписание
    {
        [Key] public int Код_расписания { get; set; }
        public int Код_сотрудника { get; set; }
        public string День_недели { get; set; }
        public TimeSpan Время_начала { get; set; }
        public TimeSpan Время_окончания { get; set; }

        [ForeignKey("Код_сотрудника")]
        public Сотрудники Сотрудники { get; set; }
    }
}

