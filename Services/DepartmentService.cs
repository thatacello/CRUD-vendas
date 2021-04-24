using System.Collections.Generic;
using System.Linq;
using Estudos_MVC_Udemy_Prof_Nelio_Alves.Data;
using Estudos_MVC_Udemy_Prof_Nelio_Alves.Models;

namespace Estudos_MVC_Udemy_Prof_Nelio_Alves.Services
{
    public class DepartmentService
    {
        private readonly ApplicationDbContext _context; // readonly -> não permite ser alterado
        public DepartmentService(ApplicationDbContext context)
        {
            _context = context;
        }
        // traz a lista de todos os departamento
        public List<Department> FindAll()
        {
            return _context.Department.OrderBy(x => x.Name).ToList();
        }
        // inserir um novo departamento no banco de dados
        public void Insert(Department obj)
        {
            _context.Add(obj);
            _context.SaveChanges();
        }
    }
}