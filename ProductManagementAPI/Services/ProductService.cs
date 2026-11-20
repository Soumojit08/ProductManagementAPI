using Microsoft.EntityFrameworkCore;
using ProductManagementAPI.Data;
using ProductManagementAPI.Dto;
using ProductManagementAPI.Models;


namespace ProductManagementAPI.Services
{
    public class ProductService(AppDbContext context) : IProductService
    {
        public async Task<ProductDto> AddProductAsync(ProductDto product)
        {
            ArgumentNullException.ThrowIfNull(product);

            var entity = new Product
            {
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Color = product.Color,
                Stock = product.Stock
            };

            context.products.Add(entity);
            await context.SaveChangesAsync();

            product.Id = entity.Id;
            return product;
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            var product = await context.products.FindAsync(id);

            if (product == null)
            {
                return false;
            }

            context.products.Remove(product);
            await context.SaveChangesAsync();

            return true;
        }

        public async Task<ProductDto?> GetProductByIdAsync(int id)
        {
            var product = await context.products.Select(x => new ProductDto
            {
               Id = x.Id,
               Name = x.Name,
               Description = x.Description,
               Color = x.Color,
               Price = x.Price,
               Stock = x.Stock,
            }).FirstOrDefaultAsync(x => x.Id == id);
            return product;
        }

        public async Task<List<ProductDto>> GetProductsAsync()
             => await context.products.Select(x => new ProductDto
             {
                 Id = x.Id,
                 Name = x.Name,
                 Description = x.Description,
                 Price = x.Price,
                 Color = x.Color,
                 Stock = x.Stock,
             }).ToListAsync();
        

        public async Task<bool> UpdateProductAsync(int id, ProductDto product)
        {
            var productToUpdate = await context.products.FindAsync(id);

            if (productToUpdate == null)
            {
                return false;
            }

            productToUpdate.Name = product.Name;
            productToUpdate.Description = product.Description;
            productToUpdate.Color = product.Color;
            productToUpdate.Price = product.Price;
            productToUpdate.Stock = product.Stock;

            await context.SaveChangesAsync();
            return true;
        }
    }
}
