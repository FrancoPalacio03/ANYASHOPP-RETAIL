using Application.Request;
using Application.Responce;

namespace Application.Interfaces.sale
{
    public interface ISaleService
    {
        Task<SaleResponse> CreateSale(SaleRequest saleRequest);
        Task<List<SaleGetResponse>> GetAll(DateTime? from, DateTime? to);
        Task<SaleResponse> GetSaleById(int saleId);
    }
}
