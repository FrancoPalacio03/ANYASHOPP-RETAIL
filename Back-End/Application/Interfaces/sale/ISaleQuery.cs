using Domain.Entities;

namespace Application.Interfaces.sale
{
    public interface ISaleQuery
    {
        Task<List<Sale>> GetSaleList(DateTime? from, DateTime? to);
        Task<Sale> GetSaleById(int SaleId);
    }
}
