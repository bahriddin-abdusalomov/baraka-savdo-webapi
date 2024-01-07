using Baraka_Savdo.Domain.Entities.Users;
using Baraka_Savdo.Service.Dtos.Users;
using Baraka_Savdo.Service.Interfaces.Users;

using Microsoft.AspNetCore.Mvc;

namespace Baraka_Savdo.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly int maxPageSize = 30;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromForm] UserCreateDto userCreateDto)
        {
            bool result = await _userService.CreateAsync(userCreateDto);
            return Ok(result);
        }

        [HttpGet("user")]
        public async Task<IActionResult> GetByIdAsync([FromForm] long id)
        {
            User user = await _userService.GetByIdAsync(id);
            return Ok(user);
        }

        [HttpGet("users")]
        public async Task<IActionResult> GetAllAsync([FromForm] int page)
        {
            var users = await _userService.GetAllAsync(new DataAccess.Utils.PaginationParams(page, maxPageSize));
            return Ok(users);
        }

        [HttpGet("count")]
        public async Task<IActionResult> CountAsync()
        {
            long count = await _userService.CountAsync();
            return Ok(count);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAsync([FromForm] long id)
        {
            bool result = await _userService.DeleteAsync(id); 
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromForm] long id, UserUpdateDto userUpdateDto)
        {
            bool result = await _userService.UpdateAsync(id, userUpdateDto);
            return Ok(result);
        }
    }
}
