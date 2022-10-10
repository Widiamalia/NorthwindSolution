using Northwind.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Domain.Repositories
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> GetAllOrder(bool trackChanges);

        Task<Order> GetOrderById(int orderId, bool trackChanges);

        Task<Order> FilterCustId(string custId, bool trackchanges);

        void Insert(Order order);
        void Edit(Order order);
        void Remove(Order order);
    }
}
