using Application.Interfaces.IMappers;
using Application.Responce;
using Domain.Entities;

namespace Application.Mappers
{
    public class SaleProductMapper : ISaleProductMapper
    {
        private readonly IProductMapper _productMapper;

        public SaleProductMapper(IProductMapper productMapper)
        {
            _productMapper = productMapper;
        }

        public async Task<SaleProductResponse> GetSaleProductResponse(SaleProduct saleProduct)
        {

            var response = new SaleProductResponse
            {
                Id = saleProduct.ShoppingCartId,
                productId = saleProduct.Product.ToString(),
                Quantity = saleProduct.Quantity,
                Price = saleProduct.Price,
                Discount = saleProduct.Discount
            };

            return await Task.FromResult(response);
        }

        public async Task<List<SaleProductResponse>> GetSaleProductsResponseDictionary(Sale sale, Dictionary<ProductResponse, int> productsWithQuantities)
        {
            List<SaleProductResponse> saleProductResponses = new List<SaleProductResponse>();

            foreach (KeyValuePair<ProductResponse, int> product in productsWithQuantities)
            {
                SaleProduct saleProduct = new SaleProduct
                {
                    Product = product.Key.Id,
                    Sale = sale.SaleId,
                    Quantity = product.Value,
                    Price = product.Key.Price,
                    Discount = product.Key.Discount
                };
                var productResponse = await GetSaleProductResponse(saleProduct);
                saleProductResponses.Add(productResponse);
            }

            return saleProductResponses;
        }

        public async Task<List<SaleProductResponse>> GetSaleProductsResponse(List<SaleProduct> saleProducts)
        {
            List<SaleProductResponse> saleProductResponses = new List<SaleProductResponse>();

            foreach (var saleProductData in saleProducts)
            {
                SaleProduct saleProduct = new SaleProduct
                {
                    Product = saleProductData.Product,
                    Sale = saleProductData.sale.SaleId,
                    Quantity = saleProductData.Quantity,
                    Price = saleProductData.Price,
                    Discount = saleProductData.Discount
                };
                var productResponse = await GetSaleProductResponse(saleProduct);
                saleProductResponses.Add(productResponse);
            }

            return saleProductResponses;
        }

    }
}