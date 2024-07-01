using Application.Interfaces.IMappers;
using Application.Responce;
using Domain.Entities;

namespace Application.Mappers
{
    public class CategoryMapper : ICategoryMapper
    {
        public async Task<category> GetCategoryResponse(Category _category)
        {
            category category = new category
            {
                Id = _category.CategoryId,
                Name = _category.Name
            };
            return await Task.FromResult(category);
        }
    }
}
