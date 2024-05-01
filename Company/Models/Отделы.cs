using System;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Company.Models
{
    public class Отделы
    {
        [Key] public int Код_отдела { get; set; }
        public string Название_отдела { get; set; }
        public int Код_организации { get; set; }

        [ForeignKey("Код_организации")]
        public Организации Организации { get; set; }
    }
}

