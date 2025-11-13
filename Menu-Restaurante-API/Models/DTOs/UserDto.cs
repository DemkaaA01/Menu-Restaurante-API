namespace Menu_Restaurante_API.Models.DTOs
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;  
        public string Email { get; set; } = string.Empty;

        public string RestaurantName { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string? Phone { get; set; }

        public bool IsActive { get; set; }
    }
}
