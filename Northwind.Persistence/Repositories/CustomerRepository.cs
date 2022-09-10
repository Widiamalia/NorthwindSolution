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
    internal class CustomerRepository : RepositoryBase<Customer>, ICustomerRepository
    {
        public CustomerRepository(NorthwindContext dbContext) : base(dbContext)
        {
        }

        public void Edit(Customer customer)
        {
            Update(customer);
        }

        public async Task<IEnumerable<Customer>> GetAllCustomer(bool trackChanges)
        {
            return await FindAll(trackChanges).OrderBy(c => c.CustomerId).ToListAsync();
        }

        public async Task<Customer> GetCustomerById(string customerid, bool trackChanges)
        {
            return await FindByCondition(c => c.CustomerId.Equals(customerid), trackChanges).SingleOrDefaultAsync();
        }

        public bool GetCustomerById(Func<object, bool> p)
        {
            throw new NotImplementedException();
        }

        public void Insert(Customer customer)
        {
            Create(customer);
        }

        public void Remove(Customer customer)
        {
            Delete(customer);
        }
    }
}
