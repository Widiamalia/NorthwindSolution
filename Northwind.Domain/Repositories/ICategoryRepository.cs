using Northwind.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Domain.Repositories
{
    public interface ICategoryRepository
    {
        //trackChanges => feature untuk mendeteksi perubahan data object category
        Task<IEnumerable<Category>> GetAllCategory(bool trackChanges);

        Task<Category> GetCategoryById(int id, bool v);

        void Insert(Category category);

        void Edit(Category category);

        void Remove(Category category);
        bool GetCategoryById(Func<object, bool> p);
    }
}
