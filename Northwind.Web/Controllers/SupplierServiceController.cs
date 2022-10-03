using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Northwind.Contracts.Dto.Supplier;
using Northwind.Domain.Models;
using Northwind.Persistence;
using Northwind.Services.Abstraction;

namespace Northwind.Web.Controllers
{
    public class SupplierServiceController : Controller
    {
        private readonly NorthwindContext _context;
        private readonly IServiceManager _serviceContext;

        public SupplierServiceController(NorthwindContext context, IServiceManager serviceContext)
        {
            _context = context;
            _serviceContext = serviceContext;
        }

        // GET: SupplierService
        public async Task<IActionResult> Index()
        {
            return View(await _serviceContext.SupplierService.GetAllSupplier(false));
        }

        // GET: SupplierService/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            /*var supplier = await _context.Suppliers
                .FirstOrDefaultAsync(m => m.SupplierId == id);*/
            var supplier = await _serviceContext.SupplierService.GetSupplierById((int)id, false);

            if (supplier == null)
            {
                return NotFound();
            }

            return View(supplier);
        }

        // GET: SupplierService/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SupplierService/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SupplierId,CompanyName,ContactName,ContactTitle,Address,City,Region,PostalCode,Country,Phone,Fax,HomePage")] Supplier supplier)
        {
            if (ModelState.IsValid)
            {
                _context.Add(supplier);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(supplier);
        }

        // GET: SupplierService/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            /*var supplier = await _context.Suppliers.FindAsync(id);*/

            var supplier = await _serviceContext.SupplierService.GetSupplierById((int)id, true);

            if (supplier == null)
            {
                return NotFound();
            }
            return View(supplier);
        }

        // POST: SupplierService/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SupplierId,CompanyName,ContactName,ContactTitle,Address,City,Region,PostalCode,Country,Phone,Fax,HomePage")] Supplier supplier)
        {
            if (id != supplier.SupplierId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(supplier);
                    await _context.SaveChangesAsync();

                }
                catch (DbUpdateConcurrencyException)
                {
                    /*if (!SupplierExists(supplier.SupplierId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }*/
                }
                return RedirectToAction(nameof(Index));
            }
            return View(supplier);
        }

        // GET: SupplierService/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            /*var supplier = await _context.Suppliers
                .FirstOrDefaultAsync(m => m.SupplierId == id);*/

            var supplier = await _serviceContext.SupplierService.GetSupplierById((int)id, false);

            if (supplier == null)
            {
                return NotFound();
            }

            return View(supplier);
        }

        // POST: SupplierService/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            /*var supplier = await _context.Suppliers.FindAsync(id);
            _context.Suppliers.Remove(supplier);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));*/

            var supplier = await _serviceContext.SupplierService.GetSupplierById((int)id, false);
            _serviceContext.SupplierService.Remove(supplier);
            return RedirectToAction(nameof(Index));
        }

/*        private bool SupplierExists(int id)
        {
            return _context.Suppliers.Any(e => e.SupplierId == id);
        }*/
    }
}
