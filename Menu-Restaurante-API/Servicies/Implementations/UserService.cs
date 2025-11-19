using Menu_Restaurante_API.Entities;
using Menu_Restaurante_API.Models.DTOs;
using Menu_Restaurante_API.Repositories.Interfaces;
using Menu_Restaurante_API.Servicies.Interfaces;

namespace Menu_Restaurante_API.Servicies.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        void IUserService.Delete(int userId)
        {
            var user = _userRepository.GetById(userId);
            if (user == null)
                throw new KeyNotFoundException("El usuario no existe.");

            _userRepository.Remove(userId);
        }

        List<RestaurantListDto> IUserService.GetAllRestaurants()
        {
            var users = _userRepository.GetAll();

            return users
                .Select(u => new RestaurantListDto
                {
                    Id = u.Id,
                    RestaurantName = u.RestaurantName,
                    Address = u.Address,
                    Email = u.Email
                })
                .ToList();
        }

        UserDto IUserService.GetById(int userId)
        {
            var user = _userRepository.GetById(userId);
            if (user == null)
                throw new KeyNotFoundException("El usuario no existe.");

            return MapToDto(user);
        }

        UserDto IUserService.Login(LoginDto dto)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));

            var user = _userRepository.ValidateUser(dto.Email, dto.Password);
            if (user == null)
                throw new UnauthorizedAccessException("Usuario o contraseña incorrectos.");

            return MapToDto(user);
        }

        UserDto IUserService.Register(RegisterUserDto dto)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));

            if (string.IsNullOrWhiteSpace(dto.Email) ||
                string.IsNullOrWhiteSpace(dto.Password) ||
                string.IsNullOrWhiteSpace(dto.Username))
            {
                throw new ArgumentException("Email, usuario y contraseña son obligatorios.");
            }

            var user = new User
            {
                Username = dto.Username,
                Password = dto.Password,
                Email = dto.Email,
                RestaurantName = dto.RestaurantName,
                Address = dto.Address,
                Phone = dto.Phone,
            };

            var createdId = _userRepository.Create(user);
            var created = _userRepository.GetById(createdId);

            return MapToDto(created ?? user);
        }

        UserDto IUserService.Update(int userId, UserDto dto)
        {
            var user = _userRepository.GetById(userId);
            if (user == null)
                throw new KeyNotFoundException("El usuario no existe.");

            if (!string.IsNullOrWhiteSpace(dto.Username))
                user.Username = dto.Username;

            if (!string.IsNullOrWhiteSpace(dto.Email))
                user.Email = dto.Email;

            if (!string.IsNullOrWhiteSpace(dto.RestaurantName))
                user.RestaurantName = dto.RestaurantName;

            if (!string.IsNullOrWhiteSpace(dto.Address))
                user.Address = dto.Address;

            if (!string.IsNullOrWhiteSpace(dto.Phone))
                user.Phone = dto.Phone;

            if (!string.IsNullOrWhiteSpace(dto.Password))
                user.Password = dto.Password;

            _userRepository.Update(user, userId);

            var updated = _userRepository.GetById(userId);
            return MapToDto(updated ?? user);
        }
        private static UserDto MapToDto(User user)
        {
            return new UserDto
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                RestaurantName = user.RestaurantName,
                Address = user.Address,
                Phone = user.Phone,
            };
        }
    }
}
