using Baraka_Savdo.DataAccess.Utils;
using Baraka_Savdo.Domain.Entities.Products;
using Baraka_Savdo.Service.Dtos.Categories;
using Baraka_Savdo.Service.Dtos.Products;
using Baraka_Savdo.Service.Interfaces.Products;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Baraka_Savdo.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly int maxPageSize = 30;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllAsync([FromQuery] int page)
        {
            var products = await _productService.GetAllAsync(new PaginationParams(page, maxPageSize));
            return Ok(products);
        }

        [HttpGet]
        public async Task<IActionResult> GetByIdAsync(long id)
        {
            Product product = await _productService.GetByIdAsync(id);
            return Ok(product);
        }

        [HttpGet]
        public async Task<IActionResult> CountAsync()
        {
            long count = await _productService.CountAsync();
            return Ok(count);
        }

        [HttpPost]
        public async Task<ActionResult> CreateAsync([FromForm] ProductCreateDto product)
        {
            bool result = await _productService.CreateAsync(product);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync(long id, [FromForm] ProductUpdateDto product)
        {
            bool result = await _productService.UpdateAsync(id, product);
            return Ok(result);
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteAsync(long categoryId)
        {
            bool result = await _productService.DeleteAsync(categoryId);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> SearchAsync(string search, [FromQuery] int page)
        {
            var products = await _productService.SearchAsync(search, new PaginationParams(page, maxPageSize));
            return Ok(products);
        }
    }
}
