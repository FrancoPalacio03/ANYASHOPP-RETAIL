using Domain.Entities;

namespace Application.Interfaces.sale
{
    public interface ISaleCommand
    {
        Task<Sale> InsertSale(Sale sale);
        Task UpdateSale(Sale sale);
        Task RemoveSale(Sale sale);
    }
}
