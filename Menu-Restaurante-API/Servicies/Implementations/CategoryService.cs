using Menu_Restaurante_API.Entities;
using Menu_Restaurante_API.Models.DTOs;
using Menu_Restaurante_API.Repositories.Interfaces;
using Menu_Restaurante_API.Servicies.Interfaces;

namespace Menu_Restaurante_API.Servicies.Implementations
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        CategoryDto ICategoryService.CreateForUser(int userId, CreateCategoryDto dto)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));
            if (string.IsNullOrWhiteSpace(dto.Name))
                throw new ArgumentException("El nombre de la categoría es obligatorio.");

            var category = new Category
            {
                Name = dto.Name,
                Description = dto.Description ?? string.Empty,
                IsActive = true,
                UserId = userId
            };

            var createdId = _categoryRepository.Create(category);
            var created = _categoryRepository.GetById(createdId);

            return MapToDto(created ?? category);
        }

        CategoryWithProductsDto ICategoryService.GetWithProducts(int userId, int categoryId)
        {
            // Traemos la categoría del repo (ideal: con Include de Products)
            var category = _categoryRepository.GetById(categoryId);

            if (category == null || category.UserId != userId)
                throw new KeyNotFoundException("La categoría no existe para este restaurante.");

            return new CategoryWithProductsDto
            {
                Id = category.Id,
                Name = category.Name,
                //UserId = category.UserId,
                Products = category.Products
                    .Select(p => new ProductDto
                    {
                        Id = p.Id,
                        Name = p.Name,
                        Description = p.Description,
                        Price = p.Price,
                        DiscountPercent = p.DiscountPercent,
                        HappyHourEnabled = p.HappyHourEnabled,
                        CategoryId = p.CategoryId,
                        //UserId = p.UserId
                    })
                    .ToList()
            };
        }



        void ICategoryService.Delete(int categoryId)
        {
            var category = _categoryRepository.GetById(categoryId);
            if (category == null)
                throw new KeyNotFoundException("La categoría no existe.");

            _categoryRepository.Remove(categoryId);
        }

        CategoryDto ICategoryService.GetById(int categoryId)
        {
            var category = _categoryRepository.GetById(categoryId);
            if (category == null)
                throw new KeyNotFoundException("La categoría no existe.");

            return MapToDto(category);
        }

        List<CategoryDto> ICategoryService.GetByUser(int userId)
        {
            var categories = _categoryRepository.GetByUser(userId);
            return categories.Select(MapToDto).ToList();
        }

        CategoryDto ICategoryService.Update(int categoryId, UpdateCategoryDto dto)
        {
            var category = _categoryRepository.GetById(categoryId);
            if (category == null)
                throw new KeyNotFoundException("La categoría no existe.");

            if (!string.IsNullOrWhiteSpace(dto.Name))
                category.Name = dto.Name;

            if (dto.Description != null)
                category.Description = dto.Description;

            if (dto.IsActive.HasValue)
                category.IsActive = dto.IsActive.Value;

            _categoryRepository.Update(category, categoryId);

            var updated = _categoryRepository.GetById(categoryId);
            return MapToDto(updated ?? category);
        }
        // =====================  PRIVADOS  =====================

        private static CategoryDto MapToDto(Category category)
        {
            return new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description,
                IsActive = category.IsActive,
                UserId = category.UserId
            };
        }
    }
}
