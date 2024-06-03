using System;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Company.Models
{
    public class Сотрудники_приложения
    {
        [Key] public int Код_сотрудников_приложения { get; set; }
        public int Код_приложения { get; set; }

        public int Код_сотрудника { get; set; }
        [ForeignKey("Код_сотрудника")]
        public Сотрудники Сотрудники { get; set; }

        [ForeignKey("Код_приложения")]
        public Приложения Приложения { get; set; }
    }
}
