
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Northwind.Contracts.Dto.Order;
using Northwind.Contracts.Dto.OrderDetail;
using Northwind.Contracts.Dto.Product;
using Northwind.Services.Abstraction;
using System;
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
        //[Authorize (Roles ="Manager")]
        public async Task<ActionResult> Index()
        {
            var productOnSales = await _serviceManager.
                ProductService.GetProductOnSales(false);

            return View(productOnSales);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder(ProductDto productDto)
        {
            if(ModelState.IsValid)
            {
                //create order dan new order detail, bila customer belum melakukan order
                var products = productDto;
                var order = new OrderForCreateDto
                {
                    OrderDate = DateTime.Now,
                    RequiredDate = DateTime.Now.AddDays(3),
                    CustomerId = "WIDI"
                };

                var orders = await _serviceManager.OrderService.FilterCustId(order.CustomerId, false);
                if (orders == null)
                {
                    var createOrder = _serviceManager.OrderService.CreateOrderId(order);
                    var orderDetail = new OrderDetailForCreateDto
                    {
                        ProductId = products.ProductId,
                        OrderId = createOrder.OrderId,
                        UnitPrice = (decimal)products.UnitPrice,
                        Quantity = Convert.ToInt16(products.QuantityPerUnit),
                        Discount = 0
                    };

                    _serviceManager.OrderDetailService.Insert(orderDetail);
                    return RedirectToAction("Checkout", new { id = createOrder.OrderId });
                }

                //melakukan order lagi jika product dan shipped date null
                else
                {
                    OrderDetailDto orderDetails = new OrderDetailDto();
                    orderDetails = await _serviceManager.OrderDetailService.GetOrderDetail(orders.OrderId, products.ProductId, false);
                    if (orders.ShippedDate == null)
                    {
                        var orderDetail = new OrderDetailForCreateDto
                        {
                            ProductId = products.ProductId,
                            OrderId = orders.OrderId,
                            Quantity = Convert.ToInt16(products.QuantityPerUnit),
                            UnitPrice = (decimal)products.UnitPrice * Convert.ToInt16(products.QuantityPerUnit),
                            Discount = 0
                        };

                        if (orderDetails != null)
                        {
                            //melakukan edit jika product yang di order sama
                            if (orderDetails.ProductId == products.ProductId)
                            {
                                var newQuantity = Convert.ToInt16(products.QuantityPerUnit);
                                orderDetails.OrderId = orderDetail.OrderId;
                                orderDetails.ProductId = orderDetail.ProductId;
                                orderDetails.Quantity += newQuantity;
                                orderDetails.UnitPrice += (decimal)products.UnitPrice + newQuantity;
                                _serviceManager.OrderDetailService.Edit(orderDetails);
                                return RedirectToAction("CartItem", "OrderDetails", new { area = "" });
                            }
                        }

                        else
                        {
                            _serviceManager.OrderDetailService.Insert(orderDetail);
                            return RedirectToAction("CartItem", "OrderDetails", new { area = "" });
                        }

                        _serviceManager.OrderDetailService.Insert(orderDetail);
                        return RedirectToAction("CartItem", "OrderDetails", new { area = "" });

                    }

                    else
                    {
                        var createOrder = _serviceManager.OrderService.CreateOrderId(order);
                        var orderDetail = new OrderDetailForCreateDto
                        {
                            ProductId = products.ProductId,
                            OrderId = createOrder.OrderId,
                            UnitPrice = (decimal)products.UnitPrice,
                            Quantity = Convert.ToInt16(products.QuantityPerUnit),
                            Discount = 0
                        };
                        _serviceManager.OrderDetailService.Insert(orderDetail);
                        return RedirectToAction("CartItem", "OrderDetails", new { area = "" });
                    }
                }
            }

            return View(productDto);
        }

        // GET: ProductOnSaleController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productOnSale = await _serviceManager.ProductService.GetProductPhotoOnSalesById((int) id, false);
            if (productOnSale == null)
            {
                return NotFound();
            }
            return View(productOnSale);
        }

        public async Task<ActionResult> Checkout(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productOnSale = await _serviceManager.OrderService.GetOrderById((int) id, false);
            if (productOnSale == null)
            {
                return NotFound();
            }
            return View(productOnSale);
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
