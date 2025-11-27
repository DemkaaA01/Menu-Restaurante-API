namespace Menu_Restaurante_API.Models.DTOs
{
    public class VisitReportDto
    {
        public int UserId { get; set; }
        public string RestaurantName { get; set; } = string.Empty;
        public int Visits { get; set; }
    }
}
