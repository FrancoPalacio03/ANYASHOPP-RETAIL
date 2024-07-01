using Application.Interfaces.product;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Querys
{
    public class ProductQuery : IProductQuery
    {
        private readonly AppDbContext _appDbContext;

        public ProductQuery(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<List<Product>> GetListProducts(string? name, int? limit, int? offset, int[]? categories)
        {

            IQueryable<Product> query = _appDbContext.Products.Include(c => c.category);

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(p => p.Name.Contains(name));
            }

            if (categories != null && categories.Length > 0)
            {
                query = query.Where(p => categories.Contains(p.Category));
            }

            if (offset > 0)
            {
                query = query.Skip(offset.Value);
            }
            if (limit > 0)
            {
                query = query.Take(limit.Value);
            }


            return await query.ToListAsync();
        }


        public async Task<Product> GetProduct(Guid productId)
        {
            var product = await _appDbContext.Products
                        .Include(p => p.category)
                        .FirstOrDefaultAsync(p => p.ProductId == productId);
            return product;

        }

        public async Task<Product> GetProductByName(String productName)
        {
            var product = await _appDbContext.Products
                        .Include(p => p.category)
                        .FirstOrDefaultAsync(p => p.Name == productName);
            return product;

        }
    }
}
