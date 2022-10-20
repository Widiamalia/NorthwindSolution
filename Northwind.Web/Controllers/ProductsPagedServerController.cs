using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Northwind.Contracts.Dto.Product;
using Northwind.Domain.Models;
using Northwind.Persistence;
using Northwind.Services.Abstraction;
using X.PagedList;

namespace Northwind.Web.Controllers
{
    public class ProductsPagedServerController : Controller
    {
        private readonly NorthwindContext _context;
        private readonly IServiceManager _serviceContext;
        private readonly IUtilityService _utilityService;

        public ProductsPagedServerController(NorthwindContext context, IServiceManager serviceContext, IUtilityService utilityService)
        {
            _context = context;
            _serviceContext = serviceContext;
            _utilityService = utilityService;
        }


        // GET: ProductsPagedServer
        public async Task<IActionResult> Index(string searchString,
             string currentFilter, string sortOrder, int? page, int? fetchSize)

        {
            var pageIndex = page ?? 1;
            var pageSize = fetchSize ?? 5;

            //keep state searching value
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var productDtos = await _serviceContext
                .ProductService
                .GetProductPaged(pageIndex, pageSize, false);

            switch (sortOrder)
            {
                case "UnitPrice":
                    productDtos = productDtos.OrderByDescending(s => s.UnitPrice);
                    break;
                case "name_desc":
                    productDtos = productDtos.OrderByDescending(s => s.ProductName);
                    break;
                case "UnitOnOrder":
                    productDtos = productDtos.OrderBy(s => s.UnitsOnOrder);
                    break;
                default:
                    productDtos = productDtos.OrderBy(s => s.ProductName);
                    break;

            }

            var totalRows = productDtos.Count();
           
            if (!string.IsNullOrEmpty(searchString))
            {
                productDtos = productDtos
                    .Where(p => p.ProductName.ToLower().Contains(searchString) || p.Category.CategoryName.ToLower().Contains(searchString));

            }

            var productDtosPaged =
                   new StaticPagedList<ProductDto>(productDtos, pageIndex +1, pageSize - (pageSize-1), totalRows);

            ViewBag.PagedList = new SelectList(new List<int> {8, 15, 20 });

            ViewData["CurrentFilter"] = searchString;
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["PriceSortParm"] = string.IsNullOrEmpty(sortOrder) ? "unitPrice" : "";
            ViewData["DataSortParm"] = sortOrder == "Cate" ? "UnitInOrder" : "Cate";

            return View(productDtosPaged);
        }


        [HttpPost]
        public async Task<IActionResult> CreateProductPhoto(ProductPhotoGroupDto productPhotoGroupDto)
        {
            if (ModelState.IsValid)
            {
                var productPhotoGroup = productPhotoGroupDto;
                var listPhoto = new List<ProductPhotoCreateDto>();

                foreach (var itemPhoto in productPhotoGroup.AllPhoto)
                {
                    var fileName = _utilityService.UploadSingleFile(itemPhoto);
                    var photo = new ProductPhotoCreateDto
                    {
                        PhotoFilename = fileName,
                        PhotoFileSize = (short?)itemPhoto.Length,
                        PhotoFileType = itemPhoto.ContentType
                    };

                    listPhoto.Add(photo);
                }
                _serviceContext.ProductService
                    .CreateProductManyPhoto(productPhotoGroupDto.ProductForCreateDto, listPhoto);

            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName");
            ViewData["SupplierId"] = new SelectList(_context.Suppliers, "SupplierId", "CompanyName");


            return View("Create");

        }

            /*        public async Task<IActionResult> CreateProductPhoto(ProductPhotoGroupDto productPhotoDto)
                    {

                        var latestProductId = _serviceContext.ProductService.CreateProductId(productPhotoDto.ProductForCreateDto);
                        if (ModelState.IsValid)
                        {
                            try
                            {
                                var file = productPhotoDto.AllPhoto;
                                var folderName = Path.Combine("Resources", "images");
                                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                                if (file.Count > 0)
                                {
                                    foreach (var item in file)
                                    {
                                        var fileName = ContentDispositionHeaderValue.Parse(item.ContentDisposition).FileName.Trim('"');
                                        var fullPath = Path.Combine(pathToSave, fileName);
                                        var dbPath = Path.Combine(folderName, fileName);
                                        using (var stream = new FileStream(fullPath, FileMode.Create))
                                        {
                                            item.CopyTo(stream);
                                        }

                                        var convertSize = (Int16)item.Length;

                                        var productPhoto = new ProductPhotoCreateDto
                                        {
                                            PhotoFilename = fileName,
                                            PhotoFileType = item.ContentType,
                                            PhotoFileSize = (byte)convertSize,
                                            PhotoProductId = latestProductId.ProductId
                                        };
                                        _serviceContext.ProductPhotoService.Insert(productPhoto);

                                    }
                                    return RedirectToAction(nameof(Index));

                                    var productGroup = new ProductPhotoGroupDto
                                    {
                                        ProductForCreateDto = productPhotoDto.ProductForCreateDto,
                                        Photo1 = productPhotoDto.Photo1,
                                        Photo2 = productPhotoDto.Photo2,
                                        Photo3 = productPhotoDto.Photo3
                                    };
                                }
                            }
                            catch (Exception ex)
                            {
                                throw;
                            }
                        }
                        return View("Create");
                    }*/


            // GET: ProductsPagedServer/Details/5
            public async Task<IActionResult> Details(int? id)
            {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Category)
                //.Include(p => p.Supplier)
                .FirstOrDefaultAsync(m => m.ProductId == id);


            if (product == null)
            {
                return NotFound();
            }

            return View(product);
            }

        // GET: ProductsPagedServer/Create
        public async Task<IActionResult> Create()
        {
            var allCategory = await _serviceContext.CategoryService.GetAllCategory(false);
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName");
            ViewData["SupplierId"] = new SelectList(_context.Suppliers, "SupplierId", "CompanyName");
            return View();
        }

        // POST: ProductsPagedServer/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,ProductName,SupplierId,CategoryId,QuantityPerUnit,UnitPrice,UnitsInStock,UnitsOnOrder,ReorderLevel,Discontinued")] ProductForCreateDto product)
        {
            if (ModelState.IsValid)
            {
                _serviceContext.ProductService.Insert(product);

                /*_context.Add(product);
                await _context.SaveChangesAsync()*/

                return RedirectToAction(nameof(Index));
            }
            var allCategory = await _serviceContext.CategoryService.GetAllCategory(false);
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName", product.CategoryId);
            ViewData["SupplierId"] = new SelectList(_context.Suppliers, "SupplierId", "CompanyName", product.SupplierId);
            return View(product);
        }

        // GET: ProductsPagedServer/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);

            //var product = await _serviceContext.ProductService.GetProductById((int)id, true);

            if (product == null)
            {
                return NotFound();
            }
            var allCategory = await _serviceContext.CategoryService.GetAllCategory(false);
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName", product.CategoryId);
            ViewData["SupplierId"] = new SelectList(_context.Suppliers, "SupplierId", "CompanyName", product.SupplierId);
            return View(product);
        }

        // POST: ProductsPagedServer/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,ProductName,SupplierId,CategoryId,QuantityPerUnit,UnitPrice,UnitsInStock,UnitsOnOrder,ReorderLevel,Discontinued")] ProductDto product)
        {
            if (id != product.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.ProductId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            var allCategory = await _serviceContext.CategoryService.GetAllCategory(false);
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName", product.CategoryId);
            ViewData["SupplierId"] = new SelectList(_context.Suppliers, "SupplierId", "CompanyName", product.SupplierId);
            return View(product);
        }

        // GET: ProductsPagedServer/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.Supplier)
                .FirstOrDefaultAsync(m => m.ProductId == id);


            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: ProductsPagedServer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products.FindAsync(id);
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.ProductId == id);
        }
    }
}
