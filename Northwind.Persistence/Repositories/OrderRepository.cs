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
    public class OrderRepository : RepositoryBase<Order>, IOrderRepository
    {
        public OrderRepository(NorthwindContext dbContext) : base(dbContext)
        {
        }

        public void Edit(Order order)
        {
            Update(order);
        }

        public async Task<Order> FilterCustId(string custId, bool trackchanges)
        {
            return await FindByCondition(x => x.CustomerId.Equals(custId), trackchanges)
                .Where(a => a.CustomerId == custId && a.ShippedDate == null)
                .Include(c => c.Customer)
                .Include(e => e.Employee)
                .Include(od => od.OrderDetails)
                .SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<Order>> GetAllOrder(bool trackChanges)
        {
            return await FindAll(trackChanges)
                .Where(s => s.ShippedDate == null)
                .OrderBy(x => x.OrderId)
                .Include(c => c.Customer)
                .Include(e => e.Employee)
                .Include(od => od.OrderDetails)
                .ToListAsync();
        }

        public async Task<Order> GetOrderById(int orderId, bool trackChanges)
        {
            return await FindByCondition(x => x.OrderId.Equals(orderId), trackChanges)
                .Include(c => c.Customer)
                .Include(e => e.Employee)
                .Include(od => od.OrderDetails)
                .SingleOrDefaultAsync();
        }

        public void Insert(Order order)
        {
            Create(order);
        }

        public void Remove(Order order)
        {
            Delete(order);
        }
    }
}
