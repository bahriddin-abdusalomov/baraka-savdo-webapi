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

        public CategoriesController(ICategoryService service)
        {
            this._service = service;
        }
        [HttpPost]
        public async Task<ActionResult> CreateAsync([FromForm] CategoryCreateDto category) =>   Ok(await _service.CreateAsync(category));
        
    }
}
