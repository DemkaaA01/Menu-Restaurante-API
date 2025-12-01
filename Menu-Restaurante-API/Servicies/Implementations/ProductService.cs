using Menu_Restaurante_API.Entities;
using Menu_Restaurante_API.Models.DTOs;
using Menu_Restaurante_API.Repositories.Interfaces;
using Menu_Restaurante_API.Servicies.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace Menu_Restaurante_API.Servicies.Implementations
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        ProductDto IProductService.CreateForUser(CreateProductDto dto, int userId)
        {
           if (dto.Name == null) throw new ArgumentException("El nombre es obligatorio.");
           if (dto.Price < 0) throw new ArgumentException("El precio no puede ser menor a 0");
            if (dto.DiscountPercent < 0 || dto.DiscountPercent > 100) throw new ArgumentException("El descuento debe estar entre 0 y 100.");

            var product = new Product
            {
                Name = dto.Name,
                Price = dto.Price,
                DiscountPercent = dto.DiscountPercent,
                HappyHourEnabled = dto.HappyHourEnabled,
                Description = dto.Description ?? string.Empty,
                CategoryId = dto.CategoryId,
                UserId = userId
            };
            var createdId = _productRepository.Create(product);
            var withCategory = _productRepository.GetById(createdId);
            return MapToDto(withCategory ?? product);

        }

        void IProductService.Delete(int productId)
        {
            var existe = _productRepository.GetById(productId);
            if (existe == null) throw new KeyNotFoundException("El producto no existe.");
            _productRepository.Remove(productId);
        }

        ProductDto IProductService.GetById(int productId)
        {
            var product = _productRepository.GetById(productId); 
            if (product == null) throw new KeyNotFoundException("El producto no existe.");
            return MapToDto(product); //ese map to DTO carga los datos desde la entitie a el DTO, asi trabajamos con el como corresponde.
        }

        List<ProductDto> IProductService.GetMenu(int userId, int? categoryId, bool discounted, bool onlyFavorites)
        {
            IEnumerable<Product> products;
            if (categoryId.HasValue)
            {
                products = _productRepository.GetByCategory(userId, categoryId.Value);
            }
            else if (discounted)
            {
                products = _productRepository.GetWithDiscount(userId);
            }
            else
            {
                products = _productRepository.GetByRestaurant(userId);
            }
            return (List<ProductDto>)MapToDto(products);
        }

        ProductDto IProductService.SetDiscount(int productId, int percent)
        {
            if (percent < 0 || percent > 100) throw new ArgumentException("El descuento debe estar entre 0 y 100.");

            var product = _productRepository.GetById(productId);
            if (product == null) throw new KeyNotFoundException("Producto no encontrado.");

            product.DiscountPercent = percent;
            _productRepository.Update(product, productId);

            var withCategory = _productRepository.GetById(product.Id);
            return MapToDto(withCategory ?? product);
        }

        ProductDto IProductService.ToggleHappyHour(int productId, bool enabled)
        {
            var product = _productRepository.GetById(productId);
            if (product == null) throw new KeyNotFoundException("Producto no encontrado.");

            product.HappyHourEnabled = !product.HappyHourEnabled;
            _productRepository.Update(product, productId);

            var withCategory = _productRepository.GetById(product.Id);
            return MapToDto(withCategory ?? product);
        }

        List<ProductDto> IProductService.GetByRestaurant(int userId, int? categoryId = null, bool discounted = false)
        {
            var products = _productRepository.GetByFilter(userId, categoryId, discounted);

            return products.Select(p => MapToDto(p)).ToList();
        }

        ProductDto IProductService.Update(int productId, UpdateProductDto dto)
        {
            var product = _productRepository.GetById(productId);
            if (product == null) throw new KeyNotFoundException("El producto no existe.");

            if (dto.Name != null) product.Name = dto.Name;
            if (dto.Description != null) product.Description = dto.Description;
            if (dto.Price != null)  product.Price = dto.Price.Value;
            if (dto.DiscountPercent != null)
            {
                if (dto.DiscountPercent.Value < 0 && dto.DiscountPercent.Value > 100)
                    throw new ArgumentException("El descuento debe estar entre 0 y 100.");
                product.DiscountPercent = dto.DiscountPercent.Value;
            }
            if (dto.HappyHourEnabled.HasValue) product.HappyHourEnabled = dto.HappyHourEnabled.Value;
            if (dto.CategoryId.HasValue) product.CategoryId = dto.CategoryId.Value;

            _productRepository.Update(product, productId);
            var withCategory = _productRepository.GetById(product.Id);
            return MapToDto(withCategory ?? product);
        }
        public void IncreasePrices(int userId, int percent)
        {
            if (percent < 0) throw new ArgumentException("El porcentaje no puede ser negativo");
            _productRepository.IncreasePrices(userId, percent);
        }


        /// //////////////////////privado
        private ProductDto MapToDto(Product p)
        {
            if (p == null) return null;

            return new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                DiscountPercent = p.DiscountPercent,
                HappyHourEnabled = p.HappyHourEnabled,
                CategoryId = p.CategoryId,
                CategoryName = p.Category != null ? p.Category.Name : null
            };
        }

        private IEnumerable<ProductDto> MapToDto(IEnumerable<Product> products)
        {
            foreach (var p in products)
                yield return MapToDto(p);
        }

        
    }
    }
