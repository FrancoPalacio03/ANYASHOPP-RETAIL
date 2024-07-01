using Application.Exceptions;
using Application.Interfaces.category;
using Application.Interfaces.IMappers;
using Application.Interfaces.product;
using Application.Interfaces.saleProduct;
using Application.Request;
using Application.Responce;
using Domain.Entities;

namespace Application.UseCases
{
    public class ProductService : IProductService
    {
        private readonly IProductCommand _productCommand;
        private readonly IProductQuery _productQuery;
        private readonly IProductMapper _productMapper;
        private readonly ICategoryMapper _categoryMapper;
        private readonly ICategoryService _categoryService;
        private readonly ISaleProductService _saleProductService;
        public ProductService(IProductCommand productCommand, IProductQuery query, IProductMapper productMapper, ICategoryMapper categoryMapper, ICategoryService categoryService, ISaleProductService saleProductService)
        {
            _productCommand = productCommand;
            _productQuery = query;
            _productMapper = productMapper;
            _categoryMapper = categoryMapper;
            _categoryService = categoryService;
            _saleProductService = saleProductService;
        }

        public async Task<ProductResponse> CreateProduct(ProductRequest request)
        {
            try
            {

                if (string.IsNullOrEmpty(request.Name) || request.Discount < 0 || request.Price < 0)
                {
                    throw new BadRequest("Bad Request");
                }

                if (await _productQuery.GetProductByName(request.Name) != null)
                {
                    throw new Conflict("Product with this name already exists");
                }

                await CategoryCheck(request);


                var product = new Product
                {
                    Name = request.Name,
                    Description = request.Description,
                    Price = request.Price,
                    Category = request.Category,
                    Discount = request.Discount,
                    ImageUrl = request.imageUrl
                };

                var insertedProduct = await _productCommand.InsertProduct(product);

                var productResponse = await _productMapper.GetProductResponse(insertedProduct);

                return productResponse;
            }
            catch (Conflict ex)
            {
                throw new Conflict(ex.Message);
            }
            catch (BadRequest ex)
            {
                throw new BadRequest(ex.Message);
            }


        }

        public async Task<List<ProductGetResponse>> GetAll(string? name, int? limit, int? offset, int[]? categories)
        {
            List<Product> products = await _productQuery.GetListProducts(name, limit, offset, categories);
            return await _productMapper.GetProductGetResponse(products); ;
        }

        public async Task<ProductResponse> GetProductById(Guid productId)
        {
            try
            {
                if (await CheckProductId(productId))
                {
                    throw new NotFoundException("ID error");
                }
                var product = await _productQuery.GetProduct(productId);
                var productResponse = await _productMapper.GetProductResponse(product);
                return productResponse;
            }
            catch (NotFoundException ex)
            {
                throw new NotFoundException(ex.Message);
            }
        }

        public async Task<ProductResponse> UpdateProduct(Guid id, ProductRequest request)
        {
            try
            {
                var existingProduct = await _productQuery.GetProduct(id);
                if (existingProduct == null)
                {
                    throw new NotFoundException("Product not exist");
                }

                if (string.IsNullOrEmpty(request.Name) || request.Discount < 0 || request.Price < 0)
                {
                    throw new BadRequest("Bad Request");
                }

                var productWithSameName = await _productQuery.GetProductByName(request.Name);
                if (productWithSameName != null && productWithSameName.ProductId != id)
                {
                    throw new Conflict("Product with this name already exists");
                }

                await CategoryCheck(request);

                var product = new Product
                {
                    Name = request.Name,
                    Description = request.Description,
                    Price = request.Price,
                    Category = request.Category,
                    Discount = request.Discount,
                    ImageUrl = request.imageUrl
                };
                var productNew = await _productCommand.UpdateProduct(product, id);
                var productResponse = await _productMapper.GetProductResponse(productNew);
                return productResponse;

            }
            catch (NotFoundException ex)
            {
                throw new NotFoundException(ex.Message);
            }
            catch (BadRequest ex)
            {
                throw new BadRequest(ex.Message);
            }
            catch (Conflict ex)
            {
                throw new Conflict(ex.Message);
            }
        }


        public async Task<ProductResponse> DeleteProduct(Guid productId)
        {
            try
            {

                var existingProduct = await _productQuery.GetProduct(productId);
                if (existingProduct == null)
                {
                    throw new NotFoundException("Product not found");
                }

                if (await _saleProductService.IsProductInAnySale(productId))
                {
                    throw new Conflict("Product cannot be deleted as it is part of an existing sale.");
                }

                await _productCommand.RemoveProduct(existingProduct);

                var productResponse = await _productMapper.GetProductResponse(existingProduct);
                return productResponse;
            }
            catch (NotFoundException ex)
            {
                throw new NotFoundException(ex.Message);
            }
            catch (Conflict ex)
            {
                throw new Conflict(ex.Message);
            }

        }

        private async Task<bool> CheckProductId(Guid id)
        {
            return await _productQuery.GetProduct(id) == null;
        }

        private async Task CategoryCheck(ProductRequest request)
        {
            try
            {
                Category category = await _categoryService.GetById(request.Category);
            }
            catch (NotFoundException ex)
            {
                throw new BadRequest(ex.Message);
            }

        }


    }
}
