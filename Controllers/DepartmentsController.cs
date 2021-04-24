using Estudos_MVC_Udemy_Prof_Nelio_Alves.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Estudos_MVC_Udemy_Prof_Nelio_Alves.Services;
using Estudos_MVC_Udemy_Prof_Nelio_Alves.Models.ViewModels;
using Estudos_MVC_Udemy_Prof_Nelio_Alves.Models;
using Estudos_MVC_Udemy_Prof_Nelio_Alves.Services.Exceptions;
using System.Diagnostics;

namespace Estudos_MVC_Udemy_Prof_Nelio_Alves.Controllers
{
    public class DepartmentsController : Controller
    {
        private readonly SellerService _sellerService;
        private readonly DepartmentService _departmentService;
        public DepartmentsController(SellerService sellerService, DepartmentService departmentService) // injeção de dependência
        {
            _sellerService = sellerService;
            _departmentService = departmentService;
        }
        public async Task<IActionResult> Index()
        {
            var list = await _departmentService.FindAllAsync();
            return View(list);
        }
        public async Task<IActionResult> Create()
        {
            var sellers = await _sellerService.FindAllAsync();
            var viewModel = new DepartmentFormViewModel { Sellers = sellers };
            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken] // prevenção de ataques
        public async Task<IActionResult> Create(Department department)
        {
            // é necessário fazer as validações aqui também
            // pois caso o JS esteja desabilitado, impedirá de enviar o formulário vazio
            if(!ModelState.IsValid) // testa de o modelo foi validado
            {
                var sellers = await _sellerService.FindAllAsync();
                var viewModel = new DepartmentFormViewModel { Department = department , Sellers = sellers };
                return View(viewModel);
            }
            await _departmentService.InsertAsync(department);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int? id) // ? -> opcional
        {
            if(id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }
            var obj = await _departmentService.FindByIdAsync(id.Value);
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
            try
            {
                await _departmentService.RemoveAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch(IntegrityException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
            
        }
        public async Task<IActionResult> Details(int? id)
        {
            if(id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }
            var obj = await _departmentService.FindByIdAsync(id.Value);
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
            var obj = await _departmentService.FindByIdAsync(id.Value);
            if(obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }
            List<Seller> sellers = await _sellerService.FindAllAsync();
            DepartmentFormViewModel viewModel = new DepartmentFormViewModel { Department = obj, Sellers = sellers };
            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken] // prevenção de ataques
        public async Task<IActionResult> Edit(int id, Department department)
        {
            // é necessário fazer as validações aqui também
            // pois caso o JS esteja desabilitado, impedirá de enviar o formulário vazio
            if(!ModelState.IsValid) // testa de o modelo foi validado
            {
                var sellers = await _sellerService.FindAllAsync();
                var viewModel = new DepartmentFormViewModel { Department = department , Sellers = sellers };
                return View(viewModel);
            }
            if(id != department.Id)
            {
                return RedirectToAction(nameof(Error), new { message = "Id mismatch" });
            }
            try
            {
                await _departmentService.UpdateAsync(department);
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