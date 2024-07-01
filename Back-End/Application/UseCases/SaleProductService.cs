using Application.Interfaces.IMappers;
using Application.Interfaces.saleProduct;
using Application.Responce;
using Domain.Entities;

namespace Application.UseCases
{
    public class SaleProductService : ISaleProductService
    {
        private readonly ISaleProductQuery _saleProductQuery;
        private readonly ISaleProductMapper _saleProductMapper;

        public SaleProductService(ISaleProductQuery query, ISaleProductMapper saleProductMapper)
        {
            _saleProductQuery = query;
            _saleProductMapper = saleProductMapper;
        }

        public async Task<List<SaleProduct>> GetAll()
        {
            List<SaleProduct> saleProducts = await _saleProductQuery.GetListSaleProducts();
            return saleProducts;
        }

        public async Task<SaleProduct> GetById(int saleProductId)
        {
            var saleProduct = await _saleProductQuery.GetSaleProduct(saleProductId);
            return saleProduct;
        }

        public async Task<List<SaleProductResponse>> GetSaleProductsBySaleId(int id)
        {
            List<SaleProduct> saleProducts = await _saleProductQuery.GetSaleProductsBySaleId(id);
            List<SaleProductResponse> saleProductResponses = await _saleProductMapper.GetSaleProductsResponse(saleProducts);
            return saleProductResponses;
        }

        public async Task<List<SaleProduct>> ReturnSaleProducts(Dictionary<ProductResponse, int> productsWithQuantity, Sale sale)
        {
            List<SaleProduct> saleProducts = new List<SaleProduct>();
            foreach (var saleProduct in productsWithQuantity)
            {
                SaleProduct saleProductCreated = new SaleProduct
                {
                    Product = saleProduct.Key.Id,
                    Sale = sale.SaleId,
                    Quantity = saleProduct.Value,
                    Price = saleProduct.Key.Price,
                    Discount = saleProduct.Key.Discount,
                };
                saleProducts.Add(saleProductCreated);
            }
            return await Task.FromResult(saleProducts);
        }

        public async Task<bool> IsProductInAnySale(Guid productId)
        {
            return await _saleProductQuery.IsProductInAnySale(productId);
        }

    }
}
