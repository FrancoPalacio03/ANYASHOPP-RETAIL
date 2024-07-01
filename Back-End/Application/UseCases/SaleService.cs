using Application.Exceptions;
using Application.Interfaces.IMappers;
using Application.Interfaces.product;
using Application.Interfaces.sale;
using Application.Interfaces.saleProduct;
using Application.Request;
using Application.Responce;
using Domain.Entities;

namespace Application.UseCases
{
    public class SaleService : ISaleService
    {
        private readonly ISaleCommand _saleCommand;
        private readonly ISaleQuery _saleQuery;
        private readonly ISaleProductService _saleProductServices;
        private readonly ISaleMapper _saleMapper;
        private readonly IProductService _productService;

        public SaleService(ISaleCommand command, ISaleQuery query, ISaleProductService saleProductServices, ISaleMapper saleMapper, IProductService productService)
        {
            _saleCommand = command;
            _saleQuery = query;
            _saleProductServices = saleProductServices;
            _saleMapper = saleMapper;
            _productService = productService;
        }
        public async Task<SaleResponse> CreateSale(SaleRequest saleRequest)
        {
            try
            {
                if (saleRequest.TotalPayed <= 0)
                {
                    throw new BadRequest("Bad Request");
                }

                int totalQuantity = 0;

                if (saleRequest.Products == null)
                {
                    throw new BadRequest("Bad Request");
                }


                Dictionary<ProductResponse, int> productsWithQuantity = await ObtainProducts(saleRequest.Products);



                Sale saleCreated = await ReturnSale(productsWithQuantity);

                List<SaleProduct> saleProducts = await _saleProductServices.ReturnSaleProducts(productsWithQuantity, saleCreated);
                saleCreated.SaleProducts = saleProducts;


                if (saleRequest.TotalPayed == saleCreated.TotalPay)
                {
                    foreach (KeyValuePair<ProductResponse, int> product in productsWithQuantity)
                    {
                        if (product.Value <= 0) { throw new BadRequest("Total Pay calculate error"); }
                        totalQuantity += product.Value;
                    }
                    var createdSale = await _saleCommand.InsertSale(saleCreated);
                }
                else
                {
                    throw new BadRequest("Total Pay calculate error");
                }

                return await _saleMapper.GetSaleResponse(saleCreated, totalQuantity); ;

            }
            catch (BadRequest ex)
            {
                throw new BadRequest(ex.Message);
            }
        }

        public async Task<List<SaleGetResponse>> GetAll(DateTime? from, DateTime? to)
        {
            try
            {
                if (from.HasValue && to.HasValue && from.Value > to.Value)
                {
                    throw new BadRequest("Bad Request");
                }
                List<Sale> sale = await _saleQuery.GetSaleList(from, to);
                return await _saleMapper.GetSaleGetResponse(sale);
            }
            catch (BadRequest ex)
            {
                throw new BadRequest(ex.Message);
            }
        }

        public async Task<SaleResponse> GetSaleById(int saleId)
        {
            try
            {
                var sale = await _saleQuery.GetSaleById(saleId);

                if (sale == null)
                {
                    throw new NotFoundException("Sale not found");
                }

                List<SaleProductResponse> saleProductResponses = await _saleProductServices.GetSaleProductsBySaleId(saleId);
                int totalQuantity = 0;
                foreach (SaleProductResponse saleProductResponse in saleProductResponses)
                {
                    totalQuantity += saleProductResponse.Quantity;
                }

                return await _saleMapper.GetSaleResponse(sale, totalQuantity);

            }
            catch (NotFoundException ex)
            {
                throw new NotFoundException(ex.Message);
            }
        }

        private async Task<Sale> ReturnSale(Dictionary<ProductResponse, int> productsWithQuantity)
        {
            decimal subtotal = CalculateSubTotal(productsWithQuantity);
            decimal totalDiscount = CalculateTotalDiscount(productsWithQuantity);
            decimal totalPay = Math.Round(CalculateTotal(subtotal, totalDiscount), 2);
            decimal taxes = totalPay * 0.21m;
            int totalQuantity = 0;


            foreach (KeyValuePair<ProductResponse, int> product in productsWithQuantity)
            {
                totalQuantity += product.Value;
            }
            var saleCreated = new Sale
            {
                TotalPay = totalPay,
                Subtotal = subtotal,
                TotalDiscount = totalDiscount,
                Taxes = taxes,
                Date = DateTime.Now,
            };

            return await Task.FromResult(saleCreated);
        }


        private decimal CalculateTotal(decimal subTotal, decimal totalDiscount)
        {
            decimal total = (subTotal - totalDiscount) * 1.21m;
            return total;
        }

        private decimal CalculateSubTotal(Dictionary<ProductResponse, int> productsAndQuantities)
        {
            decimal total = 0m;
            foreach (KeyValuePair<ProductResponse, int> product in productsAndQuantities)
            {
                total += product.Key.Price * product.Value;
            };
            return total;
        }

        private decimal CalculateTotalDiscount(Dictionary<ProductResponse, int> productsAndQuantities)
        {
            decimal totalDiscount = 0;
            foreach (KeyValuePair<ProductResponse, int> product in productsAndQuantities)
            {
                if (product.Key.Discount > 0)
                {
                    decimal percentage = product.Key.Discount / 100m;
                    decimal unitDiscount = product.Key.Price * percentage;
                    totalDiscount += product.Value * unitDiscount;
                }
            };
            return totalDiscount;
        }

        public async Task<Dictionary<ProductResponse, int>> ObtainProducts(List<SaleProductRequest> saleProductRequests)
        {
            Dictionary<ProductResponse, int> productsAndQuantities = new Dictionary<ProductResponse, int>();
            foreach (SaleProductRequest saleProduct in saleProductRequests)
            {
                ProductResponse currentProduct = await _productService.GetProductById(saleProduct.ProductId);
                productsAndQuantities.Add(currentProduct, saleProduct.Quantity);
            };
            return productsAndQuantities;
        }


    }
}
