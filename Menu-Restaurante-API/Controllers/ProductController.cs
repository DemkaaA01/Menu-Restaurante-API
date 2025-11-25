using Menu_Restaurante_API.Models.DTOs;
using Menu_Restaurante_API.Servicies.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Menu_Restaurante_API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    // Invitado: obtener detalle de un producto
    // GET: api/product/5
    [HttpGet("{productId:int}")]
    [AllowAnonymous]
    public ActionResult<ProductDto> GetById(int productId)
    {
        var product = _productService.GetById(productId);
        return Ok(product);
    }

    // Invitado: obtener productos de un restaurante, con filtros
    // GET: api/product/restaurant/1?categoryId=2&discounted=true&onlyFavorites=false
    [HttpGet("restaurant/{userId:int}")]
    [AllowAnonymous]
    public ActionResult<List<ProductDto>> GetByRestaurant(
        int userId,
        [FromQuery] int? categoryId = null,
        [FromQuery] bool discounted = false)
    {
        var products = _productService.GetByRestaurant(userId, categoryId, discounted);
        return Ok(products);
    }

    // Dueño: crear producto
    // POST: api/product/restaurant/1
    [HttpPost("restaurant/{userId:int}")]
    [Authorize]
    public ActionResult<ProductDto> CreateForUser(int userId, [FromBody] CreateProductDto dto)
    {
        var created = _productService.CreateForUser(dto, userId);
        // lo devuelvo con 201 y la url del recurso creado
        return CreatedAtAction(nameof(GetById), new { productId = created.Id }, created);
    }

    // Dueño: actualizar un producto
    // PUT: api/product/5
    [HttpPut("{productId:int}")]
    [Authorize]
    public ActionResult<ProductDto> Update(int productId, [FromBody] UpdateProductDto dto)
    {
        var updated = _productService.Update(productId, dto);
        return Ok(updated);
    }

    // Dueño: borrar un producto
    // DELETE: api/product/5
    [HttpDelete("{productId:int}")]
    [Authorize]
    public IActionResult Delete(int productId)
    {
        _productService.Delete(productId);
        return NoContent();
    }

    // Dueño: cambiar porcentaje de descuento de un producto
    // POST: api/product/5/discount/30
    [HttpPost("{productId:int}/discount/{percent:int}")]
    [Authorize]
    public ActionResult<ProductDto> SetDiscount(int productId, int percent)
    {
        var product = _productService.SetDiscount(productId, percent);
        return Ok(product);
    }

    // Dueño: habilitar / deshabilitar happy hour
    // POST: api/product/5/happyhour
    [HttpPost("{productId:int}/happyhour")]
    [Authorize]
    public ActionResult<ProductDto> ToggleHappyHour(int productId, bool enabled)
    {
        var product = _productService.ToggleHappyHour(productId, enabled);
        return Ok(product);
    }
}
