using System.Collections.Generic;

namespace Estudos_MVC_Udemy_Prof_Nelio_Alves.Models.ViewModels
{
    public class DepartmentFormViewModel
    {
        public Department Department { get; set; }
        public ICollection<Seller> Sellers { get; set; }
    }
}