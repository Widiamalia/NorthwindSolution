﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Northwind.Contracts.Dto.OrderDetail;
using Northwind.Domain.Models;
using Northwind.Persistence;
using Northwind.Services.Abstraction;

namespace Northwind.Web.Controllers
{
    public class OrderDetailsController : Controller
    {
        private IServiceManager _serviceManager;

        public OrderDetailsController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }


        public async Task<IActionResult> CartItem()
        {
            var custId = "WIDI";
            var cartItem = await _serviceManager.OrderDetailService.GetAllCartItem(custId, false);
            return View(cartItem);
        }

        // GET: OrderDetails
        public async Task<IActionResult> Index()
        {
            var orderDetail = await _serviceManager.OrderDetailService.GetAllOrderDetail(false);
            return View(orderDetail);
        }

        // GET: OrderDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderDetail = await _serviceManager.OrderDetailService.GetOrderDetailById((int)id, false);

            if (orderDetail == null)
            {
                return NotFound();
            }

            return View(orderDetail);
        }

        // GET: OrderDetails/Create
        public async Task<IActionResult> Create()
        {
            var allProduct = await _serviceManager.ProductService.GetAllProduct(false);
            ViewData["ProductId"] = new SelectList(allProduct, "ProductId", "ProductName");
            return View();
        }

        // POST: OrderDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderId,ProductId,UnitPrice,Quantity,Discount")] OrderDetailForCreateDto orderDetail)
        {
            if (ModelState.IsValid)
            {
                _serviceManager.OrderDetailService.Insert(orderDetail);
                return RedirectToAction(nameof(Index));
            }

            var allProduct = await _serviceManager.ProductService.GetAllProduct(false);
            ViewData["ProductId"] = new SelectList(allProduct, "ProductId", "ProductName", orderDetail.ProductId);
            return View(orderDetail);

        }

        // GET: OrderDetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderDetail = await _serviceManager.OrderDetailService.GetOrderDetailById((int)id, true);
           
            if (orderDetail == null)
            {
                return NotFound();
            }

            var allProduct = await _serviceManager.ProductService.GetAllProduct(false);
            ViewData["ProductId"] = new SelectList(allProduct, "ProductId", "ProductName", orderDetail.ProductId);
            return View(orderDetail);
        }

        // POST: OrderDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrderId,ProductId,UnitPrice,Quantity,Discount")] OrderDetailDto orderDetail)
        {
            if (id != orderDetail.OrderId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _serviceManager.OrderDetailService.Edit(orderDetail);
                }

                catch (DbUpdateConcurrencyException)
                {
                    throw;

                }
            }

            var allProduct = await _serviceManager.ProductService.GetAllProduct(false);
            ViewData["ProductId"] = new SelectList(allProduct, "ProductId", "ProductName", orderDetail.ProductId);
            return View(orderDetail);
        }

        // GET: OrderDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderDetail = await _serviceManager.OrderDetailService.GetOrderDetailById((int)id, false);

            if (orderDetail == null)
            {
                return NotFound();
            }

            return View(orderDetail);
        }

        // POST: OrderDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var orderDetail = await _serviceManager.OrderDetailService.GetOrderDetailById((int)id, false);
            _serviceManager.OrderDetailService.Remove(orderDetail);
            return RedirectToAction(nameof(Index));
        }
    }
}
