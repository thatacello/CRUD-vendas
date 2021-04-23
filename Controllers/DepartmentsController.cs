using System.Collections.Generic;
using Estudos_MVC_Udemy_Prof_Nelio_Alves.Data;
using Estudos_MVC_Udemy_Prof_Nelio_Alves.Models;
using Microsoft.AspNetCore.Mvc;

namespace Estudos_MVC_Udemy_Prof_Nelio_Alves.Controllers
{
    public class DepartmentsController : Controller
    {
        private readonly ApplicationDbContext _database;
        public DepartmentsController(ApplicationDbContext database)
        {
            _database = database;
        }
        public IActionResult Index()
        {
            List<Department> list = new List<Department>();
            list.Add(new Department { Id = 1, Name = "Eletronics" });
            list.Add(new Department { Id = 2, Name = "Fashion" });

            return View(list);
        }
    }
}