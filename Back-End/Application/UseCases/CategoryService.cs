using Application.Exceptions;
using Application.Interfaces.category;
using Domain.Entities;

namespace Application.UseCases
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryQuery _categoryQuery;

        public CategoryService(ICategoryQuery query)
        {
            _categoryQuery = query;
        }

        public async Task<List<Category>> GetAll()
        {
            return await _categoryQuery.GetListCategories();
        }

        public async Task<Category> GetById(int categoryId)
        {
            if (!await CheckCategoryId(categoryId))
            {
                throw new NotFoundException("ID error");
            }

            return await _categoryQuery.GetCategory(categoryId);
        }

        private async Task<bool> CheckCategoryId(int id)
        {
            return (await _categoryQuery.GetCategory(id) != null);
        }


    }
}
