using Application.Exceptions;
using Application.Interfaces.product;
using Application.Request;
using Application.Responce;
using Microsoft.AspNetCore.Mvc;

namespace _TP2_REST_Palacio_Franco.Controllers;
[Route("api/[controller]")]
[ApiController]
public class Product : Controller
{
    private readonly IProductService _productService;
    public Product(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(ProductGetResponse), 200)]
    public async Task<IActionResult> GetListProducts([FromQuery] int[]? categories, string? name, int limit = 0, int offset = 0)
    {
        var result = await _productService.GetAll(name, limit, offset, categories);
        return new JsonResult(result) { StatusCode = 200 };    
    }

    [HttpPost]
    [ProducesResponseType(typeof(ProductResponse), 201)]
    [ProducesResponseType(typeof(ApiError), 400)]
    [ProducesResponseType(typeof(ApiError), 409)]
    public async Task<IActionResult> CreateProduct(ProductRequest request)
    {
        try
        {
            var result = await _productService.CreateProduct(request);
            return new JsonResult(result) { StatusCode = 201 };
        }
        catch (BadRequest ex)
        {
            return new JsonResult(new ApiError { message = ex.Message }) { StatusCode = 400 };
        }

        catch (Conflict ex)
        {
            return new JsonResult(new ApiError { message = ex.Message }) { StatusCode = 409 };
        }

    }
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ProductResponse), 200)]
    [ProducesResponseType(typeof(ApiError), 404)]
    public async Task<ActionResult> GetProductById(Guid id)
    {
        try
        {
            var result = await _productService.GetProductById(id);
            return new JsonResult(result) { StatusCode = 200 };
        }

        catch (NotFoundException ex)
        {
            return new JsonResult(new ApiError { message = ex.Message }) { StatusCode = 404 };
        }


    }

    [HttpPut("{id}")]
    [ProducesResponseType(typeof(ProductResponse), 200)]
    [ProducesResponseType(typeof(ApiError), 400)]
    [ProducesResponseType(typeof(ApiError), 404)]
    [ProducesResponseType(typeof(ApiError), 409)]
    public async Task<ActionResult> UpdateProduct(Guid id, ProductRequest request)
    {
        try
        {
            var result = await _productService.UpdateProduct(id, request);
            return new JsonResult(result) { StatusCode = 200 };
        }
        catch (BadRequest ex)
        {
            return new JsonResult(new ApiError { message = ex.Message }) { StatusCode = 400 };
        }
        catch (NotFoundException ex)
        {
            return new JsonResult(new ApiError { message = ex.Message }) { StatusCode = 404 };
        }
        catch (Conflict ex)
        {
            return new JsonResult(new ApiError { message = ex.Message }) { StatusCode = 409 };
        }

    }

    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(ProductResponse), 200)]
    [ProducesResponseType(typeof(ApiError), 404)]
    [ProducesResponseType(typeof(ApiError), 409)]
    public async Task<ActionResult> DeleteProduct(Guid id)
    {
        try
        {
            var result = await _productService.DeleteProduct(id);
            return new JsonResult(result) { StatusCode = 200 };
        }
        catch (NotFoundException ex)
        {
            return new JsonResult(new ApiError { message = ex.Message }) { StatusCode = 404 };
        }
        catch (Conflict ex)
        {
            return new JsonResult(new ApiError { message = ex.Message }) { StatusCode = 409 };
        }
    }

}
