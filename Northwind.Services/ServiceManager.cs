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
        }

        public ICategoryService CategoryService => _lazyCategoryService.Value;
        public IProductService ProductService => _lazyProductService.Value;
        public IProductPhotoService ProductPhotoService => _lazyProductPhotoService.Value;
        public ISupplierService SupplierService => _lazySupplierService.Value;
    }
}
