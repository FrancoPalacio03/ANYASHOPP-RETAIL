using Domain.Entities;

namespace Application.Interfaces.saleProduct
{
    public interface ISaleProductQuery
    {
        Task<List<SaleProduct>> GetListSaleProducts();
        Task<SaleProduct> GetSaleProduct(int getSaleProductId);
        Task<List<SaleProduct>> GetSaleProductsBySaleId(int id);
        Task<bool> IsProductInAnySale(Guid productId);
    }
}
