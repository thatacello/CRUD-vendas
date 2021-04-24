using System.Runtime.Serialization;
using System.Collections.Generic;
using System.Linq;
using Estudos_MVC_Udemy_Prof_Nelio_Alves.Data;
using Estudos_MVC_Udemy_Prof_Nelio_Alves.Models;
using Microsoft.EntityFrameworkCore;
using Estudos_MVC_Udemy_Prof_Nelio_Alves.Services.Exceptions;
using System.Threading.Tasks;

namespace Estudos_MVC_Udemy_Prof_Nelio_Alves.Services
{
    public class SellerService
    {
        private readonly ApplicationDbContext _context; // readonly -> não permite ser alterado
        public SellerService(ApplicationDbContext context)
        {
            _context = context;
        }
        // traz a lista de todos os vendedores
        public async Task<List<Seller>> FindAllAsync()
        {
            return await _context.Seller.ToListAsync();
        }
        // inserir um novo vendedor no banco de dados
        public async Task InsertAsync(Seller obj)
        {
            _context.Add(obj);
            await _context.SaveChangesAsync();
        }
        public async Task<Seller> FindByIdAsync(int id)
        {
            return await _context.Seller.Include(obj => obj.Department).FirstOrDefaultAsync(obj => obj.Id == id);
        }
        public async Task RemoveAsync(int id)
        {
            var obj = await _context.Seller.FindAsync(id);
            _context.Seller.Remove(obj);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(Seller obj)
        {
            bool hasAny = await _context.Seller.AnyAsync(x => x.Id == obj.Id);

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