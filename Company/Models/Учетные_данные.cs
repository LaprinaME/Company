using System;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Company.Models
{
    public class Учетные_данные
    {
        [Key] public int Код_учетных_данных { get; set; }
        public int Код_сотрудника { get; set; }
        public int Код_статуса { get; set; }
        public DateTime Срок_действия_учетки { get; set; }
        public DateTime Последний_вход { get; set; }

        [ForeignKey("Код_сотрудника")]
        public Сотрудники Сотрудники { get; set; }

        [ForeignKey("Код_статуса")]
        public Статусы Статусы { get; set; }
    }
}
