using Domain.Entities;

namespace Application.Interfaces.category
{
    public interface ICategoryQuery
    {
        Task<List<Category>> GetListCategories();
        Task<Category> GetCategory(int categoryId);
    }
}
