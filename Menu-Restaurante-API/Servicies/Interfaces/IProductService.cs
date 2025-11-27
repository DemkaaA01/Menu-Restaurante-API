using Menu_Restaurante_API.Models.DTOs;

namespace Menu_Restaurante_API.Servicies.Interfaces
{
    public interface IProductService
    {
        List<ProductDto> GetMenu(int userId, int? categoryId = null, bool discounted = false, bool onlyFavorites = false);
        ProductDto GetById(int productId);
        ProductDto CreateForUser(CreateProductDto dto, int userId);
        ProductDto Update(int productId, UpdateProductDto dto);
        void Delete(int productId);

        ProductDto SetDiscount(int productId, int percent);        
        ProductDto ToggleHappyHour(int productId, bool enabled);
        List<ProductDto> GetByRestaurant(int userId, int? categoryId = null, bool discounted = false);
        void IncreasePrices(int userId, int percent);
    }
}
