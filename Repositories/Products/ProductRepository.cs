using Microsoft.EntityFrameworkCore;
using Repositories.DBContext;

namespace Repositories.Products
{
    public class ProductRepository(AppDbContext context) : GenericRepository<Product,int>(context), IProductRepository
    {
        public Task<List<Product>> GetTopPriceProductAsync(int count)
        {
            return Context.Products.OrderByDescending(p => p.Price).Take(count).ToListAsync();
        }
    }
}
