using Menu_Restaurante_API.Models.DTOs;

namespace Menu_Restaurante_API.Servicies.Interfaces
{
    public interface IUserService
    {
        List<RestaurantListDto> GetAllRestaurants();
        UserDto GetById(int userId);

        // Registro / login
        UserDto Register(RegisterUserDto dto);
        UserDto Login(LoginDto dto);

        // Dueño de restaurante
        UserDto Update(int userId, UserDto dto);
        void Delete(int userId);
    }
}
