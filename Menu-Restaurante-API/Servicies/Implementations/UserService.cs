using Menu_Restaurante_API.Entities;
using Menu_Restaurante_API.Helpers;
using Menu_Restaurante_API.Models.DTOs;
using Menu_Restaurante_API.Repositories.Interfaces;
using Menu_Restaurante_API.Servicies.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Menu_Restaurante_API.Servicies.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;

        public UserService(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
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
                Password = PasswordHelper.Hash(dto.Password),
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
        public LoginResponseDto Login(LoginDto dto)
        {
            // 1) Validar credenciales en el repositorio
            User user = _userRepository.ValidateUser(dto.Email, dto.Password);
            if (user == null)
            {
                throw new UnauthorizedAccessException("Email o contraseña incorrectos.");
            }

            // 2) Construir claims
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                // si más adelante querés roles, se agregan acá
            };

            // 3) Obtener configuración JWT
            var jwtSection = _configuration.GetSection("Jwt");

            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSection["Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            string expireValue = jwtSection["ExpireMinutes"] ?? "60"; // default 60
            int expireMinutes = int.Parse(expireValue);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(expireMinutes),
                Issuer = jwtSection["Issuer"],
                Audience = jwtSection["Audience"],
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            string tokenString = tokenHandler.WriteToken(securityToken);

            // 4) Armar respuesta
            return new LoginResponseDto
            {
                Token = tokenString,
                UserId = user.Id,
                Username = user.Username,
                RestaurantName = user.RestaurantName
            };
        }
        public void TrackVisit(int userId)
        {
            _userRepository.IncrementVisits(userId);
        }

        public List<VisitReportDto> GetVisitsReport()
        {
            var users = _userRepository.GetAll();

            return users
                .Select(u => new VisitReportDto
                {
                    UserId = u.Id,
                    RestaurantName = u.RestaurantName,
                    Visits = u.Visits
                })
                .ToList();
        }

        /// privado

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
