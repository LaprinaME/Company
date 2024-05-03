using System.Collections.Generic;


namespace Company.ViewModels
{
    public class ChangeOrganizationStatusViewModel
    {
        public int Код_организации { get; set; }
        public string Название_организации { get; set; }
        public int Код_статуса { get; set; }
        public int Код_родительской_организации { get; set; }
    }
}
