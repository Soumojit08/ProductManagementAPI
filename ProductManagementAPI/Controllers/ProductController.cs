using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ProductManagementAPI.Dto;
using ProductManagementAPI.Models;
using ProductManagementAPI.Services;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ProductManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]


    public class ProductController(IProductService service) : ControllerBase
    {

        [HttpGet]
        public async Task<ActionResult<List<ProductDto>>> GetProducts()
           => Ok(await service.GetProductsAsync());

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetProductsById(int id)
        {
            var product = await service.GetProductByIdAsync(id);
            
            if(product is null)
            {
                return NotFound("Product Not found");
            }

            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult<ProductDto>> PostProduct(ProductDto product)
        {
            var createProduct = await service.AddProductAsync(product);
            return CreatedAtAction(nameof(GetProductsById), new { id = createProduct.Id }, createProduct);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, ProductDto product)
        {
            var updated = await service.UpdateProductAsync(id, product);
            return updated ? NoContent() : NotFound("Product Not found");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var deleted = await service.DeleteProductAsync(id);
            return deleted ? NoContent() : NotFound("Product Not found");
        }
    }
}

