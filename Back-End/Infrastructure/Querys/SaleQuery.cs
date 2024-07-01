using Application.Interfaces.sale;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Querys
{
    public class SaleQuery : ISaleQuery
    {
        private readonly AppDbContext _appDbContext;

        public SaleQuery(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<Sale> GetSaleById(int SaleId)
        {
            var sale = await _appDbContext.FindAsync<Sale>(SaleId);
            return sale;
        }

        public async Task<List<Sale>> GetSaleList(DateTime? from, DateTime? to)
        {
            var query = _appDbContext.Sales.AsQueryable();

            if (from.HasValue)
            {
                from = from.Value.Date; // Comienzo del día (00:00:00)
                query = query.Where(s => s.Date >= from);
            }

            if (to.HasValue)
            {
                to = to.Value.Date.AddDays(1).AddTicks(-1); // Final del día (23:59:59.9999999)
                query = query.Where(s => s.Date <= to);
            }

            return await query.ToListAsync();
        }

    }
}
