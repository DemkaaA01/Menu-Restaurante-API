using Menu_Restaurante_API.Entities;

namespace Menu_Restaurante_API.Repositories.Interfaces
{
    public interface IProductRepository
    {
        Product GetById(int productId);
        List<Product> GetByRestaurant(int userId);                // menú completo
        List<Product> GetByCategory(int userId, int categoryId);  // filtrado por categoría
        List<Product> GetWithDiscount(int userId);                // con descuento/happy hour

        int Create(Product newProduct);
        void Update(Product updated, int productId);
        void Remove(int productId);

        // extras dueñx:
        void SetDiscount(int productId, int percent);
        void ToggleHappyHour(int productId, bool enabled);
        List<Product> GetByFilter(int userId, int? categoryId, bool discounted);

    }
}
