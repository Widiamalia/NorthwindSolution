using Microsoft.EntityFrameworkCore;
using Northwind.Domain.Dto;
using Northwind.Domain.Models;
using Northwind.Domain.Repositories;
using Northwind.Persistence.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Persistence.Repositories
{
    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        public ProductRepository(NorthwindContext dbContext) : base(dbContext)
        {
        }

        public void Edit(Product product)
        {
            Update(product);
        }

        public async Task<IEnumerable<Product>> GetAllProduct(bool trackChanges)
        {
            return await FindAll(trackChanges)
                .Include(c => c.Category)
                .OrderBy(p => p.ProductId)
                .Include(od => od.OrderDetails)
                .ToListAsync();
        }

        public async Task<Product> GetProductById(int productid, bool trackChanges)
        {
            return await FindByCondition(p => p.ProductId.Equals(productid), trackChanges)
                .Include(c => c.Category)
                .Include(od => od.OrderDetails)
                .SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<Product>> GetProductOnSale(bool trackChanges)
        {
            var products = await _dbContext.Products
                                 .Where(x => x.ProductPhotos
                                 .Any(y => y.PhotoProductId == x.ProductId))
                                 .Include(p => p.ProductPhotos)
                                 .ToListAsync();
            return products;

            /*var products = await FindAll(trackChanges)
                .Include(x => x.ProductPhotos.SingleOrDefault())
                .ToListAsync();
            return products;*/

            //based on method
        }

        public async Task<Product> GetProductOnSaleById(int productOnSaleId, bool trackChanges)
        {
            var productOnSale = await FindByCondition(p => p.ProductId.Equals(productOnSaleId), trackChanges)
                                 .Where(x => x.ProductPhotos
                                 .Any(y => y.PhotoProductId == productOnSaleId))
                                 .Include(c => c.Category)
                                 .Include(o => o.OrderDetails)
                                 .Include(p => p.ProductPhotos)
                                 .SingleOrDefaultAsync();
            return productOnSale;
        }

        public async Task<IEnumerable<Product>> GetProductPaged(int pageIndex, int pageSize, bool trackChanges)
        {
            return await FindAll(trackChanges)
                .OrderBy(p => p.ProductId)
                .Include(c => c.Category)
                .Skip((pageIndex -1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<Product> GetProductPhotoOnSalesById(int productOnSaleId, bool trackChanges)
        {
            var productOnSale = await FindByCondition(p => p.ProductId.Equals(productOnSaleId), trackChanges)
                                 .Where(x => x.ProductPhotos
                                 .Any(y => y.PhotoProductId == productOnSaleId))
                                 .Include(c => c.Category)
                                 .Include(p => p.ProductPhotos)
                                 .Include(od => od.OrderDetails)
                                 .SingleOrDefaultAsync();
            return productOnSale;
        }

        public async Task<Product> GetProductOrderOnSalesById(int productOnSaleId, bool trackChanges)
        {
            var productOnSale = await FindByCondition(p => p.ProductId.Equals(productOnSaleId), trackChanges)
                                 .Where(x => x.ProductPhotos
                                 .Any(y => y.PhotoProductId == productOnSaleId))
                                 .Include(c => c.Category)
                                 .Include(o => o.OrderDetails)
                                 .Include(p => p.ProductPhotos)
                                 .SingleOrDefaultAsync();
            return productOnSale;
        }

        public void Insert(Product product)
        {
            Create(product);
        }

        public void Remove(Product product)
        {
            Delete(product);
        }

        public IEnumerable<TotalProductByCategory> GetTotalProductByCategory()
        {
            var rawSQL = _dbContext.TotalProductByCategorySQL
                .FromSqlRaw("select c.CategoryName,COUNT(p.productId)TotalProduct " +
                "from Products p join Categories c on p.CategoryID = c.CategoryID " +
                "group by c.CategoryName")
                .Select(x => new TotalProductByCategory
                {
                    CategoryName = x.CategoryName,
                    TotalProduct = x.TotalProduct
                })
                .OrderBy(x => x.TotalProduct)
                .ToList();

            return rawSQL;
        }
    }
}
