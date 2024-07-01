using Application.Exceptions;
using Application.Interfaces.sale;
using Application.Request;
using Application.Responce;
using Microsoft.AspNetCore.Mvc;

namespace _TP2_REST_Palacio_Franco.Controllers;
[Route("api/[controller]")]
[ApiController]
public class Sale : Controller
{
    private readonly ISaleService _saleService;
    public Sale(ISaleService saleService)
    {
        _saleService = saleService;
    }


    [HttpGet]
    [ProducesResponseType(typeof(SaleGetResponse), 200)]
    [ProducesResponseType(typeof(ApiError), 400)]
    public async Task<ActionResult> GetListSales(DateTime? from, DateTime? to)
    {
        try
        {
            var result = await _saleService.GetAll(from, to);
            return new JsonResult(result) { StatusCode = 200 };
        }
        catch (BadRequest ex)
        {
            return new JsonResult(new ApiError { message = ex.Message }) { StatusCode = 400 };
        }
    }

    [ProducesResponseType(typeof(SaleResponse), 201)]
    [ProducesResponseType(typeof(ApiError), 400)]
    [HttpPost]
    public async Task<IActionResult> CreateSale(SaleRequest request)
    {
        try
        {
            var result = await _saleService.CreateSale(request);
            return new JsonResult(result) { StatusCode = 201 };
        }
        catch (BadRequest ex)
        {
            return new JsonResult(new ApiError { message = ex.Message }) { StatusCode = 400 };
        }
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(SaleResponse), 200)]
    [ProducesResponseType(typeof(ApiError), 404)]
    public async Task<ActionResult> GetSaleById(int id)
    {
        try
        {
            var result = await _saleService.GetSaleById(id);
            return new JsonResult(result) { StatusCode = 200 };
        }
        catch (NotFoundException ex)
        {
            return new JsonResult(new ApiError { message = ex.Message }) { StatusCode = 404 };
        }
    }
}
