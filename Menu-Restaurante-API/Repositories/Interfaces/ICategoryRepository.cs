using Menu_Restaurante_API.Entities;

namespace Menu_Restaurante_API.Repositories.Interfaces
{
    public interface ICategoryRepository
    {
        Category GetById(int categoryId);
        List<Category> GetByUser(int userId);

        int Create(Category newCategory);
        void Update(Category updated, int categoryId);
        void Remove(int categoryId);
    }
}
