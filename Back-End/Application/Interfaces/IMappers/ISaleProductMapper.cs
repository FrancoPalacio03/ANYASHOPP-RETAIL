using Application.Responce;
using Domain.Entities;

namespace Application.Interfaces.IMappers
{
    public interface ISaleProductMapper
    {
        Task<SaleProductResponse> GetSaleProductResponse(SaleProduct saleProduct);
        Task<List<SaleProductResponse>> GetSaleProductsResponseDictionary(Sale sale, Dictionary<ProductResponse, int> productsWithQuantities);
        Task<List<SaleProductResponse>> GetSaleProductsResponse(List<SaleProduct> saleProducts);
    }
}
