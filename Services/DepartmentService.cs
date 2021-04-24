using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Estudos_MVC_Udemy_Prof_Nelio_Alves.Data;
using Estudos_MVC_Udemy_Prof_Nelio_Alves.Models;
using Microsoft.EntityFrameworkCore;

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
        public async Task<List<Department>> FindAllAsync() // é um padrão adotado na linguagem c# colocar o Async no final do método
        {
            return await _context.Department.OrderBy(x => x.Name).ToListAsync();
        }
        // inserir um novo departamento no banco de dados
        public void Insert(Department obj)
        {
            _context.Add(obj);
            _context.SaveChanges();
        }
    }
}