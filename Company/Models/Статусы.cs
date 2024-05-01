using System;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Company.Models
{
    public class Статусы
    {
        [Key] public int Код_статуса { get; set; }
        public string Название_статуса { get; set; }
    }
}

