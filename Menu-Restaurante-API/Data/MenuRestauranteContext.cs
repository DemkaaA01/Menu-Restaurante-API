using Microsoft.EntityFrameworkCore;
using Menu_Restaurante_API.Entities;

namespace Menu_Restaurante_API.Data
{
    public class MenuRestauranteContext : DbContext //DbContext crea la base de datos, es decir las tablas.
    {
        public DbSet<User> Users { get; set; } //DbSet marca como se forman las tablas de la BD
        public DbSet<Product> Products { get; set; } //a partir de las propiedades de cada entitie define las columnas.
        public DbSet<Category> Categories { get; set; }
        public MenuRestauranteContext(DbContextOptions<MenuRestauranteContext> options)
            : base(options) //el constructor
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // 🔹 Seed de usuarios (restaurantes)
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Username = "Restaurante San Martín",
                    Email = "sanmartin@gmail.com",
                    Password = "12345",

                },
                new User
                {
                    Id = 2,
                    Username = "Pizzería El Rincón",
                    Email = "pizzeria@gmail.com",
                    Password = "abcd1234",
                }
            );

            // 🔹 Seed de categorías
            modelBuilder.Entity<Category>().HasData(
                // Categorías del restaurante San Martín
                new Category { Id = 1, Name = "Bebidas", IsActive = true, UserId = 1 }, //fijate que el UserId determina a quien pertenece.
                new Category { Id = 2, Name = "Postres", IsActive = true, UserId = 1 },
                new Category { Id = 3, Name = "Platos Principales", IsActive = true, UserId = 1 },

                // Categorías de Pizzería El Rincón
                new Category { Id = 4, Name = "Pizzas", IsActive = true, UserId = 2 },
                new Category { Id = 5, Name = "Empanadas", IsActive = true, UserId = 2 },
                new Category { Id = 6, Name = "Bebidas", IsActive = true, UserId = 2 }
            );

            // 🔹 Seed de productos
            modelBuilder.Entity<Product>().HasData(
                // Productos del restaurante San Martín
                new Product
                {
                    Id = 1,
                    Name = "Milanesa con papas fritas",
                    Description = "Clásica milanesa con papas fritas doradas",
                    Price = 2500,
                    DiscountPercent = 0,
                    HappyHourEnabled = false,
                    CategoryId = 3,
                    UserId = 1
                },
                new Product
                {
                    Id = 2,
                    Name = "Helado de chocolate",
                    Description = "Helado artesanal de chocolate amargo",
                    Price = 900,
                    DiscountPercent = 10,
                    HappyHourEnabled = true,
                    CategoryId = 2,
                    UserId = 1
                },
                new Product
                {
                    Id = 3,
                    Name = "Cerveza artesanal 500ml",
                    Description = "IPA rubia elaborada localmente",
                    Price = 1200,
                    DiscountPercent = 20,
                    HappyHourEnabled = true,
                    CategoryId = 1,
                    UserId = 1
                },

                // Productos de Pizzería El Rincón
                new Product
                {
                    Id = 4,
                    Name = "Pizza Margarita",
                    Description = "Mozzarella, tomate y albahaca fresca",
                    Price = 1800,
                    DiscountPercent = 0,
                    HappyHourEnabled = false,
                    CategoryId = 4,
                    UserId = 2
                },
                new Product
                {
                    Id = 5,
                    Name = "Empanada de Carne",
                    Description = "Empanada jugosa con carne cortada a cuchillo",
                    Price = 350,
                    DiscountPercent = 0,
                    HappyHourEnabled = false,
                    CategoryId = 5,
                    UserId = 2
                },
                new Product
                {
                    Id = 6,
                    Name = "Gaseosa Cola 1.5L",
                    Description = "Botella de Coca-Cola 1.5 litros",
                    Price = 800,
                    DiscountPercent = 30,
                    HappyHourEnabled = true,
                    CategoryId = 6,
                    UserId = 2
                }
            );

            base.OnModelCreating(modelBuilder);
        }
    }
}
