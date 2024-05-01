using System;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Company.Models
{
    public class Приложения
    {
        [Key] public int Код_приложения { get; set; }
        public string Название_приложения { get; set; }
        public string Тип_приложения { get; set; }
    }
}
