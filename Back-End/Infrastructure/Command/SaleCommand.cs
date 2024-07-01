using Application.Interfaces.sale;
using Domain.Entities;
using Infrastructure.Persistence;

namespace Infrastructure.Command
{
    public class SaleCommand : ISaleCommand
    {
        private readonly AppDbContext _context;

        public SaleCommand(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Sale> InsertSale(Sale sale)
        {
            _context.Sales.Add(sale);
            await _context.SaveChangesAsync();
            return sale;
        }

        public async Task UpdateSale(Sale sale)
        {
            _context.Update(sale);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveSale(Sale sale)
        {
            _context.Remove(sale);
            await _context.SaveChangesAsync();
        }
    }
}
