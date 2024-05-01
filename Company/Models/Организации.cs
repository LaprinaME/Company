using System;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Company.Models
{
    public class Организации
    {
        [Key] public int Код_организации { get; set; }
        public int Код_статуса { get; set; }
        public string Название_организации { get; set; }
        public string ИНН { get; set; }
        public string КПП { get; set; }
        public string ОГРН { get; set; }
        public int Код { get; set; }
        public string Тип_организации { get; set; }
        public DateTime Дата_закрытия { get; set; }
        public int Код_родительской_организации { get; set; }

        [ForeignKey("Код_статуса")]
        public Статусы Статусы { get; set; }

        [ForeignKey("Код_родительской_организации")]
        public Организации Родительская_организация { get; set; }
    }
}
