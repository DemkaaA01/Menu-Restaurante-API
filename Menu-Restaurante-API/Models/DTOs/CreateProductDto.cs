namespace Menu_Restaurante_API.Models.DTOs
{
    public class CreateProductDto
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int DiscountPercent { get; set; }   // 0..100
        public bool HappyHourEnabled { get; set; }
        public int CategoryId { get; set; }

    }
}
