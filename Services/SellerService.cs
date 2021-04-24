using System.Runtime.Serialization;
using System.Collections.Generic;
using System.Linq;
using Estudos_MVC_Udemy_Prof_Nelio_Alves.Data;
using Estudos_MVC_Udemy_Prof_Nelio_Alves.Models;
using Microsoft.EntityFrameworkCore;
using Estudos_MVC_Udemy_Prof_Nelio_Alves.Services.Exceptions;

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
        public List<Seller> FindAll()
        {
            return _context.Seller.Include(obj => obj.Department).ToList();
        }
        // inserir um novo vendedor no banco de dados
        public void Insert(Seller obj)
        {
            _context.Add(obj);
            _context.SaveChanges();
        }
        public Seller FindById(int id)
        {
            return _context.Seller.FirstOrDefault(obj => obj.Id == id);
        }
        public void Remove(int id)
        {
            var obj = _context.Seller.Find(id);
            _context.Seller.Remove(obj);
            _context.SaveChanges();
        }
        public void Update(Seller obj)
        {
            if(!_context.Seller.Any(x => x.Id == obj.Id))
            {
                throw new NotFoundException("Id not found");
            }
            try
            {
                _context.Update(obj);
                _context.SaveChanges();
            }
            catch(DbUpdateConcurrencyException e) // controlador conversa com a camada de serviço
            {
                throw new DbConcurrencyException(e.Message); 
            }
        }
    }
}