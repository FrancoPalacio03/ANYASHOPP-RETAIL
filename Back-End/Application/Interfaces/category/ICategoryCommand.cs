using Domain.Entities;

namespace Application.Interfaces.category;

public interface ICategoryCommand
{
    Task InsertCategory(Category category);
    Task RemoveCategory(Category category);
}
