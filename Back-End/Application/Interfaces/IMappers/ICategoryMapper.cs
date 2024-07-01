using Domain.Entities;

namespace Application.Interfaces.IMappers
{
    public interface ICategoryMapper
    {
        Task<Responce.category> GetCategoryResponse(Category category);
    }
}
