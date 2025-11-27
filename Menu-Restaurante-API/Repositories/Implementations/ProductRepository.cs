using Menu_Restaurante_API.Data;
using Menu_Restaurante_API.Entities;
using Menu_Restaurante_API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Menu_Restaurante_API.Repositories.Implementations
{
    public class ProductRepository : IProductRepository
    {
        private readonly MenuRestauranteContext _context;

        public ProductRepository (MenuRestauranteContext context)
        {
            _context = context;
        }

        public Product GetById(int productId)
        {
            return _context.Products.FirstOrDefault(x => x.Id == productId);
        }

        public List<Product> GetByRestaurant(int userId)
        {
            return _context.Products.Where(x => x.UserId == userId).ToList();
        }
        
        public List<Product> GetWithDiscount(int userId) 
        {
            return _context.Products.Where(x =>x.UserId == userId  && x.DiscountPercent > 0 || x.HappyHourEnabled).ToList();
        }
        public int Create (Product newProduct)
        {
            _context.Products.Add(newProduct);
           _context.SaveChanges();
            return newProduct.Id;
        }  
        public void Remove(int productId) 
        {
            var product = _context.Products.FirstOrDefault(x => x.Id == productId);
            if (product == null) return;

            _context.Products.Remove(product); 
            _context.SaveChanges();
        }

        public void SetDiscount(int productId, int discount)
        {
            var product = _context.Products.FirstOrDefault(x => x.Id == productId);
            if (product == null) return;

            product.DiscountPercent = discount < 0 ? 0 : discount;
            _context.SaveChanges();
        }

        public void ToggleHappyHour(int productId, bool enable) 
        {
            var product = _context.Products.FirstOrDefault(x => x.Id == productId);
            if (product == null) return;

            product.HappyHourEnabled = enable;
            _context.SaveChanges();
        }

        List<Product> IProductRepository.GetByCategory(int userId, int categoryId)
        {
            return _context.Products.Where(x => x.Id == categoryId && x.Id == userId)
                                    .Include(x => x.Category)
                                    .ToList();
        }


        void IProductRepository.Update(Product updated, int productId)
        {
            var product = _context.Products.FirstOrDefault(x => x.Id == productId);
            if (product == null) return;

            product.Name = updated.Name;
            product.Description = updated.Description;
            product.Price = updated.Price;
            product.CategoryId = updated.CategoryId;
            product.DiscountPercent = updated.DiscountPercent;
            product.HappyHourEnabled = updated.HappyHourEnabled;
            product.HappyHourStart = updated.HappyHourStart;
            product.HappyHourEnd = updated.HappyHourEnd;
        }

        List<Product> IProductRepository.GetByFilter(int userId, int? categoryId, bool discounted)
        {
            var query = _context.Products
                .Include(x => x.Category)
                .Where(x => x.UserId == userId);

            // Filtro por categoría (si viene)
            if (categoryId.HasValue)
            {
                query = query.Where(x => x.CategoryId == categoryId.Value);
            }

            // Filtro por descuento / happy hour
            if (discounted)
            {
                query = query.Where(x => x.DiscountPercent > 0 || x.HappyHourEnabled);
            }

            return query.ToList();
        }
        public void IncreasePrices(int userId, int percent)
        {
            var products = _context.Products.Where(x => x.UserId == userId).ToList();

            foreach (var p in products)
            {
                p.Price += p.Price * percent / 100;
            }

            _context.SaveChanges();
        }

    }
}
