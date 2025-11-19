namespace Menu_Restaurante_API.Models.DTOs
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int DiscountPercent { get; set; }
        public bool HappyHourEnabled { get; set; }
        public int CategoryId { get; set; }
        public bool enabled { get; set; }   
        public string? CategoryName { get; set; }   // útil para mostrar menú
    }
}
