using System;
using System.Collections.Generic;
using Estudos_MVC_Udemy_Prof_Nelio_Alves.Data;
using Estudos_MVC_Udemy_Prof_Nelio_Alves.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Estudos_MVC_Udemy_Prof_Nelio_Alves.Services
{
    public class SalesRecordService
    {
        private readonly ApplicationDbContext _context; // readonly -> não permite ser alterado
        public SalesRecordService (ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<SalesRecord>> FindByDateAsync(DateTime? minDate, DateTime? maxDate)
        {
            var result = from obj in _context.SalesRecord select obj;
            if(minDate.HasValue)
            {
                result = result.Where(x => x.Date >= minDate.Value);
            }
            if(maxDate.HasValue)
            {
                result = result.Where(x => x.Date <= maxDate.Value);
            }
            return await result.Include(x => x.Seller).Include(x => x.Seller.Department).OrderByDescending(x => x.Date).ToListAsync();
        }
    }
}