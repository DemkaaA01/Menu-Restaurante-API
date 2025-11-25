namespace Menu_Restaurante_API.Models.DTOs
{
    public class LoginResponseDto
    {
        public string Token { get; set; } = string.Empty;
        public int UserId { get; set; }
        public string Username { get; set; } = string.Empty;
        public string RestaurantName { get; set; } = string.Empty;
    }
}
