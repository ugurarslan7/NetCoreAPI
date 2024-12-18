using Microsoft.EntityFrameworkCore;
using Repositories.DBContext;

namespace Repositories.Categories
{
    public class CategoryRepository : GenericRepository<Category,int>, ICategoryRepository
    {


        public CategoryRepository(AppDbContext context) : base(context)
        {
        }

        public Task<Category?> GetCategoriyWithProductAsync(int id)
        {
            return Context.Categories.Include(p => p.Products).FirstOrDefaultAsync(p => p.Id == id);
        }

        public IQueryable<Category> GetCategoriyWithProduct()
        {
            return Context.Categories.Include(p => p.Products).AsQueryable();
        }
    }
}
