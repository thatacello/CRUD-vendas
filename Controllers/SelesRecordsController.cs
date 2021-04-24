using Microsoft.AspNetCore.Mvc;

namespace Estudos_MVC_Udemy_Prof_Nelio_Alves.Controllers
{
    public class SelesRecordsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult SimpleSearch()
        {
            return View();
        }
        public IActionResult GroupingSearch()
        {
            return View();
        }
    }
}