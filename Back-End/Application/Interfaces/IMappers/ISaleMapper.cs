using Application.Responce;
using Domain.Entities;

namespace Application.Interfaces.IMappers
{
    public interface ISaleMapper
    {
        Task<SaleResponse> GetSaleResponse(Sale sale, int totalQuantity);
        public Task<List<SaleGetResponse>> GetSaleGetResponse(List<Sale> Sale);
    }
}
