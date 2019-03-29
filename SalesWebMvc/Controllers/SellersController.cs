using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;
using SalesWebMvc.Models;
using SalesWebMvc.Models.ViewModels;
using SalesWebMvc.Services;
using SalesWebMvc.Services.Exceptions;

namespace SalesWebMvc.Controllers
{
    public class SellersController : Controller
    {
        private readonly SellerService _sellerService;
        private readonly DepartmentService _departmentService;

        public SellersController(SellerService sellerService, DepartmentService departmentService)
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
            var viewModel = new SellerFormViewModel() { Departments = departments };
            return View(viewModel);
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {
                var seller = await _sellerService.FindByIdAsync(id.Value);
                if (seller != null)
                {
                    return View(seller);
                }
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }
            return RedirectToAction(nameof(Error), new { message = "Id not provided" });
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id != null)
            {
                var seller = await _sellerService.FindByIdAsync(id.Value);
                if (seller != null)
                {
                    return View(seller);
                }
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }
            return RedirectToAction(nameof(Error), new { message = "Id not provided" });
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id != null)
            {
                var seller = await _sellerService.FindByIdAsync(id.Value);
                if (seller != null)
                {
                    List<Department> departments = await _departmentService.FindAllAsync();
                    SellerFormViewModel viewModel =
                        new SellerFormViewModel { Seller = seller, Departments = departments };
                    return View(viewModel);
                }
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }
            return RedirectToAction(nameof(Error), new { message = "Id not provided" });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Seller seller)
        {
            if (!ModelState.IsValid)
            {
                var departments = await _departmentService.FindAllAsync();
                var viewModel = new SellerFormViewModel() { Departments = departments, Seller = seller };
                return View(viewModel);
            }
            if (id != seller.Id)
            {
                return RedirectToAction(nameof(Error), new { message = "Id mismatch" });
            }

            try
            {
                await _sellerService.UpdateAsync(seller);
                return RedirectToAction(nameof(Index));
            }
            catch (ApplicationException exception)
            {

                return RedirectToAction(nameof(Error), new { message = exception.Message });
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _sellerService.RemoveAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch (IntegrityException exception)
            {
                return RedirectToAction(nameof(Error), new { Message = exception.Message });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Seller seller)
        {
            if (!ModelState.IsValid)
            {
                var departments = await _departmentService.FindAllAsync();
                var viewModel = new SellerFormViewModel() { Departments = departments, Seller = seller };
                return View(viewModel);
            }
            await _sellerService.InsertAsync(seller);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Error(string message)
        {
            var viewModel = new ErrorViewModel()
            {
                Message = message,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };
            return View(viewModel);
        }
    }
}