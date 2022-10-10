using Northwind.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Domain.Repositories
{
    public interface IShipperRepository
    {
        Task<IEnumerable<Shipper>> GetAllShipper(bool trackChanges);

        Task<Shipper> GetShipperById(int shipperId, bool trackChanges);

        void Edit(Shipper shipper);
        void Remove(Shipper shipper);
    }
}
