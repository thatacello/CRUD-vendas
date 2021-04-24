using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Estudos_MVC_Udemy_Prof_Nelio_Alves.Models
{
    public class Seller
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [DataType(DataType.EmailAddress)] // aciona o app de e-mail
        public string Email { get; set; }
        [Display(Name = "Birth Date")]
        [DataType(DataType.Date)] // não irá mais aparecer as horas
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")] // inverte dia e mês
        public DateTime BirthDate { get; set; }
        [Display(Name = "Base Salary")]
        [DisplayFormat(DataFormatString = "{0:F2}")] // deixa o salário com duas casa decimais
        public double BaseSalary { get; set; }
        public Department Department { get; set; }
        public int DepartmentId { get; set; } // avisa pro EF que não pode ser nulo
        public ICollection<SalesRecord> Sales { get; set; } = new List<SalesRecord>();
        public Seller()
        {
        }
        public Seller(int id, string name, string email, DateTime birthDate, double baseSalary, Department department)
        {
            Id = id;
            Name = name;
            Email = email;
            BirthDate = birthDate;
            BaseSalary = baseSalary;
            Department = department;
        }
        public void AddSales(SalesRecord sr) // adiciona venda
        {
            Sales.Add(sr);
        }
        public void RemoveSales(SalesRecord sr)
        {
            Sales.Remove(sr);
        }
        public double TotalSales(DateTime initial, DateTime final) // total de vendas de um vendedor
        {
            return Sales.Where(s => s.Date >= initial && s.Date <= final).Sum(s => s.Amount);
        }
    }
}