namespace Menu_Restaurante_API.Models.DTOs
{
    public class UpdateProductDto
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal? Price { get; set; }
        public int? DiscountPercent { get; set; }   // 0..100
        public bool? HappyHourEnabled { get; set; }
        public int? CategoryId { get; set; }
    }
}
