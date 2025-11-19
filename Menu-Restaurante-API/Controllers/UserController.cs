using Menu_Restaurante_API.Models.DTOs;
using Menu_Restaurante_API.Services.Interfaces;
using Menu_Restaurante_API.Servicies.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Menu_Restaurante_API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    // Invitado: obtener lista de restaurantes
    // GET: api/user/restaurants
    [HttpGet("restaurants")]
    public ActionResult<List<RestaurantListDto>> GetAllRestaurants()
    {
        var restaurants = _userService.GetAllRestaurants();
        return Ok(restaurants);
    }

    // Invitado: registrar nuevo restaurante (crear usuario dueño)
    // POST: api/user/register
    [HttpPost("register")]
    public ActionResult<UserDto> Register([FromBody] RegisterUserDto dto)
    {
        var created = _userService.Register(dto);
        return CreatedAtAction(nameof(GetById), new { userId = created.Id }, created);
    }

    // Invitado: login
    // POST: api/user/login
    [HttpPost("login")]
    public ActionResult<UserDto> Login([FromBody] LoginDto dto)
    {
        var user = _userService.Login(dto);
        // más adelante acá podrías devolver también el JWT
        return Ok(user);
    }

    // Dueño: ver su propia cuenta
    // GET: api/user/5
    [HttpGet("{userId:int}")]
    public ActionResult<UserDto> GetById(int userId)
    {
        var user = _userService.GetById(userId);
        return Ok(user);
    }

    // Dueño: editar su cuenta
    // PUT: api/user/5
    [HttpPut("{userId:int}")]
    public ActionResult<UserDto> Update(int userId, [FromBody] UserDto dto)
    {
        var updated = _userService.Update(userId, dto);
        return Ok(updated);
    }

    // Dueño: borrar su cuenta
    // DELETE: api/user/5
    [HttpDelete("{userId:int}")]
    public IActionResult Delete(int userId)
    {
        _userService.Delete(userId);
        return NoContent();
    }
}
