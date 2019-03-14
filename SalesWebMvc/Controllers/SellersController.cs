﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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
        public IActionResult Index()
        {
            var list = _sellerService.FindAll();
            return View(list);
        }

        public IActionResult Create()
        {
            var departments = _departmentService.FindAll();
            var viewModel = new SellerFormViewModel(){Departments = departments};
            return View(viewModel);
        }
        public IActionResult Delete(int? id)
        {
            if (id != null)
            {
                var seller = _sellerService.FindById(id.Value);
                if (seller != null)
                {
                    return View(seller);
                }
            }
            return NotFound();
        }

        public IActionResult Details(int? id)
        {
            if (id != null)
            {
                var seller = _sellerService.FindById(id.Value);
                if (seller != null)
                {
                    return View(seller);
                }
            }
            return NotFound();
        }
        public IActionResult Edit(int? id)
        {
            if (id != null)
            {
                var seller = _sellerService.FindById(id.Value);
                if (seller != null)
                {
                    List<Department> departments = _departmentService.FindAll();
                    SellerFormViewModel viewModel =
                        new SellerFormViewModel {Seller = seller, Departments = departments};
                    return View(viewModel);
                }
            }
            return NotFound();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Seller seller)
        {
            if (id != seller.Id)
            {
                return BadRequest();
            }

            try
            {
                _sellerService.Update(seller);
                return RedirectToAction(nameof(Index));
            }
            catch (NotFoundException exception)
            {
                return NotFound();
            }
            catch (DbConcurrencyException exception)
            {
                return BadRequest();
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            _sellerService.Remove(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Seller seller)
        {
            _sellerService.Insert(seller);
            return RedirectToAction(nameof(Index));
        }
    }
}