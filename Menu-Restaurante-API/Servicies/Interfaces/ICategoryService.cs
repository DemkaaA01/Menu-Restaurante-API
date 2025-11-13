using Menu_Restaurante_API.Models.DTOs;

namespace Menu_Restaurante_API.Servicies.Interfaces
{
    public interface ICategoryService
    {
        CategoryDto GetById(int categoryId);
        List<CategoryDto> GetByUser(int userId);

        CategoryDto CreateForUser(CreateCategoryDto dto, int userId);
        CategoryDto Update(int categoryId, UpdateCategoryDto dto);
        void Delete(int categoryId);
    }
}
