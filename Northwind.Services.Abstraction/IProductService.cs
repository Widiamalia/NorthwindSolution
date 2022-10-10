using Northwind.Contracts.Dto;
using Northwind.Contracts.Dto.Category;
using Northwind.Contracts.Dto.Order;
using Northwind.Contracts.Dto.OrderDetail;
using Northwind.Contracts.Dto.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Services.Abstraction
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetAllProduct(bool trackChanges);

        Task<ProductDto> GetProductById(int productId, bool trackChanges);

        Task<IEnumerable<ProductDto>> GetProductPaged(int pageIndex, int pageSize, bool trackChanges);

        ProductDto CreateProductId(ProductForCreateDto productForCreateDto);

        Task<IEnumerable<ProductDto>> GetProductOnSales(bool trackChanges);

        Task<ProductDto> GetProductOnSaleById(int productOnSaleId, bool trackChanges);

        Task<ProductDto> GetProductPhotoOnSalesById(int productOnSaleId, bool trackChanges);

        void CreateProductManyPhoto(ProductForCreateDto productForCreateDto,
                            List<ProductPhotoCreateDto> productPhotoCreateDtos);

        void CreateOrder(OrderForCreateDto orderForCreateDto, OrderDetailForCreateDto orderDetailForCreateDto);

        void EditProductPhoto(ProductDto productDto, List<ProductPhotoDto> productPhotoDto);

        void Insert(ProductForCreateDto productForCreateDto);

        void Edit(ProductDto productDto);

        void Remove(ProductDto productDto);
    }
}
