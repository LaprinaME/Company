using Company.Models;
using System.ComponentModel.DataAnnotations;

namespace Company.ViewModels
{
    public class EditAccessRightsViewModel
    {
        [Key] public int Код_права { get; set; }
        [Required] public int Код_роли { get; set; }
        [Required] public int Код_приложения { get; set; }
        [Required, StringLength(100)] public string Название_права { get; set; }
        [Required] public DateTime Начало_действия_доступа { get; set; }
        [Required] public DateTime Окончание_действия_доступа { get; set; }
        [Required, StringLength(50)] public string Тип_доступа { get; set; }

        // Add properties for related entities to display in the view
        public IEnumerable<Роли> Роли { get; set; }
        public IEnumerable<Приложения> Приложения { get; set; }
    }
}
