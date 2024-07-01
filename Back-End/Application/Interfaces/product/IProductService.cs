using Application.Request;
using Application.Responce;

namespace Application.Interfaces.product
{
    public interface IProductService
    {
        Task<ProductResponse> CreateProduct(ProductRequest request);
        Task<List<ProductGetResponse>> GetAll(string? name, int? limit, int? offset, int[]? categories);
        Task<ProductResponse> GetProductById(Guid productId);
        Task<ProductResponse> UpdateProduct(Guid id, ProductRequest request);
        Task<ProductResponse> DeleteProduct(Guid productId);
    }
}
