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
    public class ShipperRepository : RepositoryBase<Shipper>, IShipperRepository
    {
        public ShipperRepository(NorthwindContext dbContext) : base(dbContext)
        {
        }

        public void Edit(Shipper shipper)
        {
            Update(shipper);
        }

        public async Task<IEnumerable<Shipper>> GetAllShipper(bool trackChanges)
        {
            return await FindAll(trackChanges).OrderBy(s => s.CompanyName).ToListAsync();
        }

        public async Task<Shipper> GetShipperById(int shipperId, bool trackChanges)
        {
            return await FindByCondition(s => s.ShipperId.Equals(shipperId), trackChanges).SingleOrDefaultAsync();
        }

        public void Remove(Shipper shipper)
        {
            Delete(shipper);
        }
    }
}
