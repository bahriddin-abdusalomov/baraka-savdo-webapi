using Baraka_Savdo.DataAccess.Utils;
using Baraka_Savdo.Service.Dtos.Categories;
using Baraka_Savdo.Service.Dtos.Companies;
using Baraka_Savdo.Service.Interfaces.Categories;
using Baraka_Savdo.Service.Interfaces.Companies;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Baraka_Savdo.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly ICompanyService _service;
        private readonly int maxPageSize = 10;

        public CompaniesController(ICompanyService service)
        {
            this._service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync([FromQuery] int page)
            => Ok(await _service.GetAllAsync(new PaginationParams(page, maxPageSize)));

        [HttpGet]
        public async Task<IActionResult> GetByIdAsync(long id)
            => Ok(await _service.GetByIdAsync(id));

        [HttpGet]
        public async Task<IActionResult> CountAsync()
            => Ok(await _service.CountAsync());

        [HttpPost]
        public async Task<ActionResult> CreateAsync([FromForm] CompanyCreateDto company)
            => Ok(await _service.CreateAsync(company));

        [HttpPut]
        public async Task<IActionResult> UpdateAsync(long id, [FromForm] CompanyUpdateDto company)
            => Ok(await _service.UpdateAsync(id, company));

        [HttpDelete]
        public async Task<ActionResult> DeleteAsync(long companyId)
            => Ok(await _service.DeleteAsync(companyId));
    }
}
