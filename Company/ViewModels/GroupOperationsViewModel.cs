using System.ComponentModel.DataAnnotations;

namespace Company.ViewModels
{
    public class GroupOperationsViewModel
    {
        public int Код_статуса { get; set; }
        public int Код_организации { get; set; }
        public int Код_сотрудника { get; set; }
        public int Код_учетных_данных { get; set; }
        public int Код_права { get; set; }
    }
}
