using Domain.Entities;

namespace Application.Interfaces.product
{
    public interface IProductQuery
    {
        Task<List<Product>> GetListProducts(string? name, int? limit, int? offset, int[]? categories);
        Task<Product> GetProduct(Guid productId);
        Task<Product> GetProductByName(String productName);
    }
}
