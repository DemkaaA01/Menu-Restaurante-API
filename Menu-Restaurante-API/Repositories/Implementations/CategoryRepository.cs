using Menu_Restaurante_API.Data;
using Menu_Restaurante_API.Entities;
using Menu_Restaurante_API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Menu_Restaurante_API.Repositories.Implementations
{
    public class CategoryRepository:ICategoryRepository
    {
        private readonly MenuRestauranteContext _context;

        public CategoryRepository(MenuRestauranteContext context)
        {
            _context = context;
        }
        public Category GetById(int categoryId)
        {
            return _context.Categories.FirstOrDefault(x => x.Id == categoryId);
        }
        public Category GetByIdWithProducts(int userId, int categoryId)
        {
            return _context.Categories
                .Include(c => c.Products)
                .FirstOrDefault(c => c.Id == categoryId && c.UserId == userId);
        }

        public List<Category> GetByUser(int userId)
        {
            return _context.Categories.Where(x => x.UserId == userId).ToList(); //donde el userid puesto como parametro sea el mismo que tiene esa categoria.
        }

        public int Create(Category newCategory)
        {
            _context.Categories.Add(newCategory);
            _context.SaveChanges();
            return newCategory.Id;
        }
        public void Update(Category updated, int categoryId)
        {
            var category = _context.Categories.FirstOrDefault(x => x.Id == categoryId);
            if (category == null) return;

            category.Name = updated.Name;

            _context.SaveChanges();
        }
        public void Remove(int categoryId)
        {
            var category = _context.Categories.FirstOrDefault(x => x.Id == categoryId);
            if (category == null) return;

            _context.Categories.Remove(category);
            _context.SaveChanges();

        }
    }
}
