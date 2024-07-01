using Application.Interfaces.product;
using Domain.Entities;
using Infrastructure.Persistence;

namespace Infrastructure.Command
{
    public class ProductCommand : IProductCommand
    {
        private readonly AppDbContext _context;

        public ProductCommand(AppDbContext context)
        {
            _context = context;
        }


        public async Task<Product> InsertProduct(Product product)
        {
            _context.Add(product);
            _context.Entry(product).Reference(p => p.category).Load();
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<Product> RemoveProduct(Product product)
        {
            _context.Remove(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<Product> UpdateProduct(Product product, Guid id)
        {
            var existingProduct = await _context.Products.FindAsync(id);


            existingProduct.Name = product.Name;
            existingProduct.Description = product.Description;
            existingProduct.Price = product.Price;
            existingProduct.Category = product.Category;
            existingProduct.Discount = product.Discount;
            existingProduct.ImageUrl = product.ImageUrl;

            await _context.SaveChangesAsync();

            return existingProduct;
        }
    }
}
