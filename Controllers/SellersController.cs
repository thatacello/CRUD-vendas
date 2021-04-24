using System;
using System.Diagnostics;
using System.Collections.Generic;
using Estudos_MVC_Udemy_Prof_Nelio_Alves.Models;
using Estudos_MVC_Udemy_Prof_Nelio_Alves.Models.ViewModels;
using Estudos_MVC_Udemy_Prof_Nelio_Alves.Services;
using Microsoft.AspNetCore.Mvc;
using Estudos_MVC_Udemy_Prof_Nelio_Alves.Services.Exceptions;
using System.Threading.Tasks;

namespace Estudos_MVC_Udemy_Prof_Nelio_Alves.Controllers
{
    public class SellersController : Controller
    {
        private readonly SellerService _sellerService;
        private readonly DepartmentService _departmentService;
        public SellersController(SellerService sellerService, DepartmentService departmentService) // injeção de dependência
        {
            _sellerService = sellerService;
            _departmentService = departmentService;
        }
        public async Task<IActionResult> Index()
        {
            var list = await _sellerService.FindAllAsync();
            return View(list);
        }
        public async Task<IActionResult> Create()
        {
            var departments = await _departmentService.FindAllAsync();
            var viewModel = new SellerFormViewModel { Departments = departments };
            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken] // prevenção de ataques
        public async Task<IActionResult> Create(Seller seller)
        {
            // é necessário fazer as validações aqui também
            // pois caso o JS esteja desabilitado, impedirá de enviar o formulário vazio
            if(!ModelState.IsValid) // testa de o modelo foi validado
            {
                var departments = await _departmentService.FindAllAsync();
                var viewModel = new SellerFormViewModel { Seller = seller , Departments = departments };
                return View(viewModel);
            }
            await _sellerService.InsertAsync(seller);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int? id) // ? -> opcional
        {
            if(id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }
            var obj = await _sellerService.FindByIdAsync(id.Value);
            if(obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }
            return View(obj);
        }
        [HttpPost]
        [ValidateAntiForgeryToken] // prevenção de ataques
        public async Task<IActionResult> Delete(int id)
        {
            await _sellerService.RemoveAsync(id);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Details(int? id)
        {
            if(id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }
            var obj = await _sellerService.FindByIdAsync(id.Value);
            if(obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }
            return View(obj);
        }
        // quando não tem nenhum Http, o padrão é o HttpGet
        public async Task<IActionResult> Edit(int? id) // opcional -> evitar erro
        {
            if(id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }
            var obj = await _sellerService.FindByIdAsync(id.Value);
            if(obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }
            List<Department> departments = await _departmentService.FindAllAsync();
            SellerFormViewModel viewModel = new SellerFormViewModel { Seller = obj, Departments = departments };
            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken] // prevenção de ataques
        public async Task<IActionResult> Edit(int id, Seller seller)
        {
            // é necessário fazer as validações aqui também
            // pois caso o JS esteja desabilitado, impedirá de enviar o formulário vazio
            if(!ModelState.IsValid) // testa de o modelo foi validado
            {
                var departments = await _departmentService.FindAllAsync();
                var viewModel = new SellerFormViewModel { Seller = seller , Departments = departments };
                return View(viewModel);
            }
            if(id != seller.Id)
            {
                return RedirectToAction(nameof(Error), new { message = "Id mismatch" });
            }
            try
            {
                await _sellerService.UpdateAsync(seller);
                return RedirectToAction(nameof(Index));
            }
            catch(ApplicationException e)
            {   
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
        }
        public IActionResult Error(string message) // não precisa ser assíncrona porque não tem nenhum acesso a dados
        {
            var viewModel = new ErrorViewModel
            {
                Message = message,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier // macete para pegar o Id interno da requisição
            };
            return View(viewModel);
        }
    }
}
// async views async são mais rápidas pois fazem as buscas no banco de dados
// sem necessitar parar a aplicação (de forma assíncrona)