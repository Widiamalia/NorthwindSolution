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


        public ServiceManager(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _lazyCategoryService = new Lazy<ICategoryService>(
                () => new CategoryService(repositoryManager, mapper));

            _lazyProductService = new Lazy<IProductService>(
                () => new ProductService(repositoryManager, mapper));

            _lazyProductPhotoService = new Lazy<IProductPhotoService>(
                () => new ProductPhotoService(repositoryManager, mapper));
        }

        public ICategoryService CategoryService => _lazyCategoryService.Value;
        public IProductService ProductService => _lazyProductService.Value;

        public IProductPhotoService ProductPhotoService => throw new NotImplementedException();
    }
}
