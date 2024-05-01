using System;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Company.Models
{
    public class Роли
    {
        [Key] public int Код_роли { get; set; }
        public string Название_роли { get; set; }
        public DateTime Дата_начала_действия { get; set; }
        public DateTime Дата_окончания_действия { get; set; }
    }
}

