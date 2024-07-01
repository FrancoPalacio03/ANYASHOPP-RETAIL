using Application.Responce;
using Domain.Entities;

namespace Application.Interfaces.IMappers
{
    public interface IProductMapper
    {
        public Task<ProductResponse> GetProductResponse(Product product);
        public Task<List<ProductGetResponse>> GetProductGetResponse(List<Product> products);
    }
}
