using AutoMapper;
using Northwind.Contracts.Dto;
using Northwind.Contracts.Dto.Category;
using Northwind.Contracts.Dto.Product;
using Northwind.Domain.Base;
using Northwind.Services.Abstraction;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Northwind.Services
{
    public class ProductService : IProductService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public ProductService(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }


        public void Edit(ProductDto productDto)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ProductDto>> GetAllProduct(bool trackChanges)
        {
            var productModel = await _repositoryManager
                .ProductRepository.GetAllProduct(trackChanges);
            // source = categoryModel, target categoryDto
            var productDto = _mapper.Map<IEnumerable<ProductDto>>(productModel);
            return productDto;
        }

        public async Task<ProductDto> GetProductById(int productId, bool trackChanges)
        {
            var productModel = await _repositoryManager
                 .ProductRepository.GetProductById(productId, trackChanges);
            var productDto = _mapper.Map<ProductDto>(productModel);
            return productDto;
        }

        public async Task<IEnumerable<ProductDto>> GetProductPaged(int pageIndex, int pageSize, bool trackChanges)
        {
            var productModel = await _repositoryManager
                .ProductRepository.GetProductPaged(pageIndex, pageSize, trackChanges);
            var productDto = _mapper.Map<IEnumerable<ProductDto>>(productModel);
            return productDto;
        }

        /*
       public void Insert(CategoryForCreateDto categoryForCreateDto)
       {
           throw new NotImplementedException();
       }*/

        public void Remove(ProductDto productDto)
        {
            throw new NotImplementedException();
        }
    }
}
