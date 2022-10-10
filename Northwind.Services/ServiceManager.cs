using AutoMapper;
using Northwind.Domain.Base;
using Northwind.Services.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Services
{
    public class ServiceManager : IServiceManager
    {
        private readonly Lazy<ICategoryService> _lazyCategoryService;

        private readonly Lazy<IProductService> _lazyProductService;

        private readonly Lazy<IProductPhotoService> _lazyProductPhotoService;

        private readonly Lazy<ISupplierService> _lazySupplierService;

        private readonly Lazy<IOrderService> _lazyOrderService;

        private readonly Lazy<IOrderDetailService> _lazyOrderDetailService;

        private readonly Lazy<IShipperService> _lazyshipperService;

        public ServiceManager(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _lazyCategoryService = new Lazy<ICategoryService>(
                () => new CategoryService(repositoryManager, mapper));

            _lazyProductService = new Lazy<IProductService>(
                () => new ProductService(repositoryManager, mapper));

            _lazyProductPhotoService = new Lazy<IProductPhotoService>(
                () => new ProductPhotoService(repositoryManager, mapper));

            _lazySupplierService = new Lazy<ISupplierService>(
                () => new SupplierService(repositoryManager, mapper));

            _lazyOrderService = new Lazy<IOrderService>( 
                () => new OrderService(repositoryManager, mapper));

            _lazyOrderDetailService = new Lazy<IOrderDetailService>(
                () => new OrderDetailService(repositoryManager, mapper));

            _lazyshipperService = new Lazy<IShipperService>(
                () => new ShipperService(repositoryManager, mapper));
        }

        public ICategoryService CategoryService => _lazyCategoryService.Value;
        public IProductService ProductService => _lazyProductService.Value;
        public IProductPhotoService ProductPhotoService => _lazyProductPhotoService.Value;
        public ISupplierService SupplierService => _lazySupplierService.Value;
        public IOrderService OrderService => _lazyOrderService.Value;
        public IOrderDetailService OrderDetailService => _lazyOrderDetailService.Value;
        public IShipperService ShipperService => _lazyshipperService.Value;
    }
}
