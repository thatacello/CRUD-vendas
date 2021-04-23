using System;
using System.Collections.Generic;
using System.Linq;

namespace Estudos_MVC_Udemy_Prof_Nelio_Alves.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Seller> Sellers { get; set; } = new List<Seller>();
        public Department()
        {

        }
        // não incluir coleções no construtor
        public Department(int id, string name)
        {
            Id = id;
            Name = name;
        }
        public void AddSeller(Seller seller) // adiciona vendedor
        {
            Sellers.Add(seller);
        }
        public double TotalSales(DateTime initial, DateTime final) // total de vendas
        {
            return Sellers.Sum(s => s.TotalSales(initial, final));
        }
    }
}