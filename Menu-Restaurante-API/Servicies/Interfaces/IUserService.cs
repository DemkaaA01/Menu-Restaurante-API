using Menu_Restaurante_API.Models.DTOs;

namespace Menu_Restaurante_API.Servicies.Interfaces
{
    public interface IUserService
    {
        List<RestaurantListDto> GetAllRestaurants();
        UserDto GetById(int userId);

        // Registro / login
        UserDto Register(RegisterUserDto dto);

        // Dueño de restaurante
        UserDto Update(int userId, UserDto dto);
        void Delete(int userId);
        LoginResponseDto Login(LoginDto dto);
        void TrackVisit(int userId);
        List<VisitReportDto> GetVisitsReport();
    }
}
