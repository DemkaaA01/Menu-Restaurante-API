namespace Menu_Restaurante_API.Models.DTOs
{
    public class RestaurantListDto
    {
        public int Id { get; set; }
        public string RestaurantName { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
}
