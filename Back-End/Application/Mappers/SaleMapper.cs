using Application.Interfaces.IMappers;
using Application.Interfaces.saleProduct;
using Application.Responce;
using Domain.Entities;

namespace Application.Mappers
{
    public class SaleMapper : ISaleMapper
    {
        private readonly IProductMapper _productMapper;
        private readonly ISaleProductService _saleProductService;

        public SaleMapper(IProductMapper productMapper, ISaleProductService saleProductService)
        {
            _productMapper = productMapper;
            _saleProductService = saleProductService;
        }

        public async Task<List<SaleGetResponse>> GetSaleGetResponse(List<Sale> sales)
        {
            List<SaleGetResponse> saleGetResponses = new List<SaleGetResponse>();
            foreach (Sale sale in sales)
            {
                List<SaleProductResponse> saleProductResponses = await _saleProductService.GetSaleProductsBySaleId(sale.SaleId);
                int totalQuantity = 0;
                foreach (SaleProductResponse saleProductResponse in saleProductResponses)
                {
                    totalQuantity += saleProductResponse.Quantity;
                }
                SaleGetResponse saleGetResponse = new SaleGetResponse
                {
                    Id = sale.SaleId,
                    TotalPay = sale.TotalPay,
                    TotalQuantity = totalQuantity,
                    Date = sale.Date
                };
                saleGetResponses.Add(saleGetResponse);
            }
            return saleGetResponses;
        }

        public async Task<SaleResponse> GetSaleResponse(Sale sale, int totalQuantity)
        {
            var response = new SaleResponse
            {
                Id = sale.SaleId,
                TotalPay = sale.TotalPay,
                TotalQuantity = totalQuantity,
                Subtotal = sale.Subtotal,
                TotalDiscount = sale.TotalDiscount,
                Taxes = sale.Taxes,
                Date = sale.Date,
                Products = new List<SaleProductResponse>()
            };

            foreach (var saleProduct in sale.SaleProducts)
            {
                var productResponse = new SaleProductResponse
                {
                    Id = saleProduct.ShoppingCartId,
                    productId = saleProduct.Product.ToString(),
                    Quantity = saleProduct.Quantity,
                    Price = saleProduct.Price,
                    Discount = saleProduct.Discount
                };
                response.Products.Add(productResponse);
            }
            return await Task.FromResult(response);
        }
    }
}
