using Domain.Entities;

namespace Application.Interfaces.product
{
    public interface IProductCommand
    {
        Task<Product> InsertProduct(Product product);
        Task<Product> RemoveProduct(Product product);
        Task<Product> UpdateProduct(Product product, Guid id);

    }
}
