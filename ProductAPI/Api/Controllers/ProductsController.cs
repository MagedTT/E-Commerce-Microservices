using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ProductAPI.Application.Interfaces;
using ProductAPI.Core.Models;
using CoreLib.Interfaces;


namespace ProductApi.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin")]

public class ProductsController : ControllerBase
{
    private readonly IProduct _products;
    public ProductsController(IProduct products)
    {
        _products = products;
    }

    [HttpGet]
    [AllowAnonymous]

    public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
    {
        var products = await _products.GetAllAsync();
        if (!products.Any())
            return NotFound("No Products Found");

        return Ok(products);
    }

    [HttpGet("{id}")]
    [AllowAnonymous]

    public async Task<ActionResult<Product>> GetProduct(int id)
    {
        var product = await _products.GetByIdAsync(id);

        if (product is null)
            return NotFound("No Product Found");

        return Ok(product);
    }

    [HttpPost]
    public async Task<ActionResult<ResponseMessage>> CreateProduct(Product product)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var response = await _products.CreateAsync(product);
        if (response.Status is true)
            return Ok(response);

        return BadRequest(response);
    }

    [HttpPut]
    public async Task<ActionResult<ResponseMessage>> UpdateProduct(Product product)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var response = await _products.UpdateAsync(product);
        if (response.Status is true)
            return Ok(response);

        return BadRequest(response);
    }

    [HttpDelete]
    public async Task<ActionResult<ResponseMessage>> DeleteProduct(Product product)
    {
        var response = await _products.DeleteAsync(product);
        if (response.Status is true)
            return Ok(response);

        return BadRequest(response);
    }
}