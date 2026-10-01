using abm_productos.Models.DTOs.Requests;
using abm_productos.Models.DTOs.Responses;
using abm_productos.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace abm_productos.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductService _service;

    public ProductsController(IProductService service)
    {
        _service = service;
    }

    [HttpGet]
    public IActionResult GetAllProducts()
    {
        List<ProductForReadDto> products = _service.GetAllProducts();
        return Ok(products);
    }

    [HttpGet("search")]
    public IActionResult SearchProductsByName(string name)
    {
        List<ProductForReadDto> products = _service.SearchProductsByName(name);
        return Ok(products);
    }

    [HttpGet("stats")]
    public IActionResult GetStats()
    {
        ProductStatsDto stats = _service.GetStats();
        return Ok(stats);
    }

    [HttpGet("{id}")]
    public IActionResult GetProductById(int id)
    {
        ProductForReadDto? product = _service.GetProductById(id);

        if (product == null)
        {
            return NotFound();
        }

        return Ok(product);
    }

    [HttpPost]
    public IActionResult CreateProduct(ProductForCreateDto dto)
    {
        ProductForReadDto? created = _service.CreateProduct(dto);

        if (created == null)
        {
            return Conflict("Ya existe un producto con ese nombre.");
        }

        return CreatedAtAction(nameof(GetProductById), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateProduct(int id, ProductForUpdateDto dto)
    {
        if (_service.GetProductById(id) == null)
        {
            return NotFound();
        }

        _service.UpdateProduct(id, dto);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteProduct(int id)
    {
        if (_service.GetProductById(id) == null)
        {
            return NotFound();
        }

        _service.DeleteProduct(id);
        return NoContent();
    }
}
