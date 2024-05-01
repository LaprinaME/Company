using System;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Company.Models
{
    public class Права_доступа
    {
        [Key] public int Код_права { get; set; }
        public int Код_роли { get; set; }
        public int Код_приложения { get; set; }
        public string Название_права { get; set; }
        public DateTime Начало_действия_доступа { get; set; }
        public DateTime Окончание_действия_доступа { get; set; }
        public string Тип_доступа { get; set; }

        [ForeignKey("Код_роли")]
        public Роли Роли { get; set; }

        [ForeignKey("Код_приложения")]
        public Приложения Приложения { get; set; }
    }
}

