using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Northwind.Services.Abstraction;
using System.Threading.Tasks;

namespace Northwind.Web.Controllers
{
    public class ProductOnSaleController : Controller
    {
        private IServiceManager _serviceManager;

        public ProductOnSaleController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        // GET: ProductOnSaleController
        public async Task<ActionResult> Index()
        {
            var productOnSales = await _serviceManager.
                ProductService.GetProductOnSales(false);

            return View(productOnSales);
        }

        // GET: ProductOnSaleController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ProductOnSaleController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductOnSaleController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductOnSaleController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ProductOnSaleController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductOnSaleController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ProductOnSaleController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
