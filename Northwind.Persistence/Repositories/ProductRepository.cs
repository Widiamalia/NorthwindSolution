using Microsoft.EntityFrameworkCore;
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
    internal class ProductRepository : RepositoryBase<Product>, IProductRepository
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
                .ToListAsync();
        }

        public async Task<Product> GetProductById(int productid, bool trackChanges)
        {
            return await FindByCondition(c => c.CategoryId.Equals(productid), trackChanges).SingleOrDefaultAsync();
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


        public async Task<IEnumerable<Product>> GetProductPaged(int pageIndex, int pageSize, bool trackChanges)
        {
            return await FindAll(trackChanges)
                .OrderBy(p => p.ProductId)
                .Include(c => c.Category)
                .Skip((pageIndex -1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public void Insert(Product product)
        {
            Create(product);
        }

        public void Remove(Product product)
        {
            Delete(product);
        }
    }
}
