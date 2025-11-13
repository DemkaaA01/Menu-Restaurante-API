//using Menu_Restaurante_API.Entities;
//using Menu_Restaurante_API.Models.DTOs;
//using Menu_Restaurante_API.Repositories.Interfaces;
//using Menu_Restaurante_API.Servicies.Interfaces;

//namespace Menu_Restaurante_API.Servicies.Implementations
//{
//    public class CategoryService : ICategoryService
//    {
//        private readonly ICategoryRepository _categoryRepository;

//        public CategoryService(ICategoryRepository categoryRepository)
//        {
//            _categoryRepository = categoryRepository;
//        }
//        CategoryDto ICategoryService.CreateForUser(CreateCategoryDto dto, int userId)
//        {
//            if (dto == null) throw new ArgumentNullException(nameof(dto));
//            if (string.IsNullOrWhiteSpace(dto.Name))
//                throw new ArgumentException("El nombre de la categoría es obligatorio.");

//            var category = new Category
//            {
//                Name = dto.Name,
//                Description = dto.Description ?? string.Empty,
//                IsActive = true,
//                UserId = userId
//            };

//            var createdId = _categoryRepository.Create(category);
//            var created = _categoryRepository.GetById(createdId);

//            return MapToDto(created ?? category);
//        }

//        void ICategoryService.Delete(int categoryId)
//        {
//            var category = _categoryRepository.GetById(categoryId);
//            if (category == null)
//                throw new KeyNotFoundException("La categoría no existe.");

//            _categoryRepository.Remove(categoryId);
//        }

//        CategoryDto ICategoryService.GetById(int categoryId)
//        {
//            var category = _categoryRepository.GetById(categoryId);
//            if (category == null)
//                throw new KeyNotFoundException("La categoría no existe.");

//            return MapToDto(category);
//        }

//        List<CategoryDto> ICategoryService.GetByUser(int userId)
//        {
//            var categories = _categoryRepository.GetByUser(userId);
//            return categories.Select(MapToDto).ToList();
//        }

//        CategoryDto ICategoryService.Update(int categoryId, UpdateCategoryDto dto)
//        {
//            var category = _categoryRepository.GetById(categoryId);
//            if (category == null)
//                throw new KeyNotFoundException("La categoría no existe.");

//            // Actualizamos solo lo que venga distinto de null
//            if (!string.IsNullOrWhiteSpace(dto.Name))
//                category.Name = dto.Name;

//            if (dto.Description != null)
//                category.Description = dto.Description;

//            if (dto.IsActive.HasValue)
//                category.IsActive = dto.IsActive.Value;

//            _categoryRepository.Update(category, categoryId);

//            var updated = _categoryRepository.GetById(categoryId);
//            return MapToDto(updated ?? category);
//        }
//        // =====================  PRIVADOS  =====================

//        private static CategoryDto MapToDto(Category category)
//        {
//            return new CategoryDto
//            {
//                Id = category.Id,
//                Name = category.Name,
//                Description = category.Description,
//                IsActive = category.IsActive,
//                UserId = category.UserId
//            };
//        }
//    }
//}
