using Domain.Entities;

namespace Application.Interfaces.category
{
    public interface ICategoryService
    {
        Task<List<Category>> GetAll();
        Task<Category> GetById(int categoryId);
    }
}
