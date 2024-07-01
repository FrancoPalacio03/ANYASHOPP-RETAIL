using Application.Interfaces.category;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Querys
{
    public class CategoryQuery : ICategoryQuery
    {
        private readonly AppDbContext _appDbContext;

        public CategoryQuery(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }


        public async Task<List<Category>> GetListCategories()
        {
            var categories = await _appDbContext.Categories.ToListAsync();
            return categories;
        }

        public async Task<Category> GetCategory(int categoryId)
        {

            var category = await _appDbContext.Categories.FirstOrDefaultAsync(s => s.CategoryId == categoryId);
            return category;
        }
    }
}
