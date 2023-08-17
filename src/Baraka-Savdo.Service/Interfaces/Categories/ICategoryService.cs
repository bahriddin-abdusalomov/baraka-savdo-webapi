using Baraka_Savdo.DataAccess.Utils;
using Baraka_Savdo.Domain.Entities.Categories;
using Baraka_Savdo.Service.Dtos.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Baraka_Savdo.Service.Interfaces.Categories
{
    public interface ICategoryService
    {
        public Task<bool> CreateAsync(CategoryCreateDto dto);

        public Task<bool> DeleteAsync(long categoryId);

        public Task<long> CountAsync();

        public Task<IList<Category>> GetAllAsync(PaginationParams @params);

        public Task<Category> GetByIdAsync(long categoryId);

        public Task<bool> UpdateAsync(long categoryId, CategoryUpdateDto dto);
    }
}
