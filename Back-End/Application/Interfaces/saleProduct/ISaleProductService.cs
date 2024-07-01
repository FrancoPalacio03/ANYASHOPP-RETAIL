using Application.Responce;
using Domain.Entities;

namespace Application.Interfaces.saleProduct
{
    public interface ISaleProductService
    {
        Task<List<SaleProduct>> GetAll();
        Task<List<SaleProductResponse>> GetSaleProductsBySaleId(int id);
        Task<SaleProduct> GetById(int saleProductId);
        Task<List<SaleProduct>> ReturnSaleProducts(Dictionary<ProductResponse, int> productsWithQuantity, Sale sale);
        Task<bool> IsProductInAnySale(Guid productId);
    }
}
