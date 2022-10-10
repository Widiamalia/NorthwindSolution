using Northwind.Contracts.Dto.Shipper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Services.Abstraction
{
    public interface IShipperService
    {
        Task<IEnumerable<ShipperDto>> GetAllShipper(bool trackChanges);

        Task<ShipperDto> GetShipperById(int shipperId, bool trackChanges);

        void Edit(ShipperDto shipperDto);
        void Remove(ShipperDto shipperDto);

    }
}
