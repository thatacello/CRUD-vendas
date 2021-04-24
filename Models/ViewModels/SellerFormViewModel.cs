using System.Collections.Generic;

namespace Estudos_MVC_Udemy_Prof_Nelio_Alves.Models.ViewModels
{
    public class SellerFormViewModel
    {
        // tela de cadastro de vendedor
        public Seller Seller { get; set; }
        public ICollection<Department> Departments { get; set; } // no plural -> ajuda o framework a reconhecer os dados
    }
}