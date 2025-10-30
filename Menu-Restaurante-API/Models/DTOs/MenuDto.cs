namespace Menu_Restaurante_API.Models.DTOs
{
    public class MenuDto
    {
        public int RestaurantId { get; set; }
        public string RestaurantName { get; set; } = string.Empty;
        public List<CategoryWithProductsDto> Categories { get; set; } = new();
    }
}
