    using Baraka_Savdo.DataAccess.Utils;
using Baraka_Savdo.Domain.Entities.Categories;
using Baraka_Savdo.Service.Dtos.Categories;
using Baraka_Savdo.Service.Interfaces.Categories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Baraka_Savdo.WebApi.Controllers
{
    [Route("api/categories")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _service;
        private readonly int maxPageSize = 10;

        public CategoriesController(ICategoryService service)
        {
            this._service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync([FromQuery] int page)
            => Ok(await _service.GetAllAsync(new PaginationParams(page, maxPageSize)));

        [HttpGet("{categoryId}")]
        public async Task<IActionResult> GetByIdAsync(long id)
            => Ok( await _service.GetByIdAsync(id));

        [HttpGet("count")]
        public async Task<IActionResult> CountAsync() 
            => Ok(await _service.CountAsync());

        [HttpPost]
        public async Task<ActionResult> CreateAsync([FromForm] CategoryCreateDto category) 
            => Ok(await _service.CreateAsync(category));

        [HttpPut("{categoryId}")]
        public async Task<IActionResult> UpdateAsync(long id, [FromForm] CategoryUpdateDto category)
            => Ok( await _service.UpdateAsync(id, category));

        [HttpDelete("{categoryId}")]
        public async Task<ActionResult> DeleteAsync(long categoryId) 
            => Ok(await _service.DeleteAsync(categoryId));
    }
}
