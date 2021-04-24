using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Estudos_MVC_Udemy_Prof_Nelio_Alves.Data;
using Estudos_MVC_Udemy_Prof_Nelio_Alves.Models;
using Estudos_MVC_Udemy_Prof_Nelio_Alves.Services.Exceptions;
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
        public async Task InsertAsync(Department obj)
        {
            _context.Add(obj);
            await _context.SaveChangesAsync();
        }
        public async Task<Department> FindByIdAsync(int id)
        {
            return await _context.Department.Include(obj => obj.Sellers).FirstOrDefaultAsync(obj => obj.Id == id);
        }
        public async Task RemoveAsync(int id)
        {
            try
            {
                var obj = await _context.Department.FindAsync(id);
                _context.Department.Remove(obj);
                await _context.SaveChangesAsync();
            }
            catch(DbUpdateException e)
            {
                throw new IntegrityException(e.Message);
            }
        }
        public async Task UpdateAsync(Department obj)
        {
            bool hasAny = await _context.Department.AnyAsync(x => x.Id == obj.Id);

            if(!hasAny)
            {
                throw new NotFoundException("Id not found");
            }
            try
            {
                _context.Update(obj);
                await _context.SaveChangesAsync();
            }
            catch(DbUpdateConcurrencyException e) // controlador conversa com a camada de serviço
            {
                throw new DbConcurrencyException(e.Message); 
            }
        }
    }
}