using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Categories
{
    public interface ICategoryRepository : IGenericRepository<Category,int>
    {
        Task<Category?> GetCategoriyWithProductAsync(int id);
        IQueryable<Category> GetCategoriyWithProduct();
    }
}
