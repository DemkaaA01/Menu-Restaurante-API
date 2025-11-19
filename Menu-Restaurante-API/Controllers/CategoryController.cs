using Menu_Restaurante_API.Models.DTOs;
using Menu_Restaurante_API.Servicies.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Menu_Restaurante_API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    // Invitado / dueño: obtener una categoría por id
    // GET: api/category/3
    [HttpGet("{categoryId:int}")]
    public ActionResult<CategoryDto> GetById(int categoryId)
    {
        var category = _categoryService.GetById(categoryId);
        return Ok(category);
    }

    // Invitado / dueño: obtener categorías de un restaurante
    // GET: api/category/restaurant/1
    [HttpGet("restaurant/{userId:int}")]
    public ActionResult<List<CategoryDto>> GetByUser(int userId)
    {
        var categories = _categoryService.GetByUser(userId);
        return Ok(categories);
    }

    // Invitado: obtener una categoría con sus productos
    // GET: api/category/restaurant/1/with-products/3
    [HttpGet("restaurant/{userId:int}/with-products/{categoryId:int}")]
    public ActionResult<CategoryWithProductsDto> GetWithProducts(int userId, int categoryId)
    {
        var result = _categoryService.GetWithProducts(userId, categoryId); 
        return Ok(result);
    }

    // Dueño: crear categoría para su restaurante
    // POST: api/category/restaurant/1
    [HttpPost("restaurant/{userId:int}")]
    public ActionResult<CategoryDto> CreateForUser(int userId, [FromBody] CreateCategoryDto dto)
    {
        var created = _categoryService.CreateForUser(userId, dto);
        return CreatedAtAction(nameof(GetById), new { categoryId = created.Id }, created);
    }

    // Dueño: actualizar categoría
    // PUT: api/category/3
    [HttpPut("{categoryId:int}")]
    public ActionResult<CategoryDto> Update(int categoryId, [FromBody] UpdateCategoryDto dto)
    {
        var updated = _categoryService.Update(categoryId, dto);
        return Ok(updated);
    }

    // Dueño: borrar categoría
    // DELETE: api/category/3
    [HttpDelete("{categoryId:int}")]
    public IActionResult Delete(int categoryId)
    {
        _categoryService.Delete(categoryId);
        return NoContent();
    }
}

