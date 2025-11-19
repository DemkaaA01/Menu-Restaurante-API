using Menu_Restaurante_API.Models.DTOs;

namespace Menu_Restaurante_API.Servicies.Interfaces
{
    public interface ICategoryService
    {
        CategoryDto GetById(int categoryId);
        List<CategoryDto> GetByUser(int userId);

        CategoryDto CreateForUser(int userId, CreateCategoryDto dto);
        CategoryDto Update(int categoryId, UpdateCategoryDto dto);
        void Delete(int categoryId);

        CategoryWithProductsDto GetWithProducts(int userId, int categoryId);
    }
}
