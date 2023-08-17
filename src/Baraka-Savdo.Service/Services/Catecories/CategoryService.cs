using Baraka_Savdo.DataAccess.Interfaces.Categories;
using Baraka_Savdo.DataAccess.Utils;
using Baraka_Savdo.Domain.Entities.Categories;
using Baraka_Savdo.Service.Common.Helpers;
using Baraka_Savdo.Service.Dtos.Categories;
using Baraka_Savdo.Service.Interfaces.Categories;
using Baraka_Savdo.Service.Interfaces.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baraka_Savdo.Service.Services.Catecories
{
    public class CategoryService : ICategoryService 
    {
        private readonly ICategoryRepository _repository;
        private readonly IFileService _fileService;

        public CategoryService( ICategoryRepository categoryRepository,  IFileService fileService)
        {
            this._repository = categoryRepository;
            this._fileService = fileService;
        }
        public Task<long> CountAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> CreateAsync(CategoryCreateDto dto)
        {
            string imagePath = await _fileService.UploadAvatarAsync(dto.ImagePath);
            Category category = new Category()
            {
                ImagePath = imagePath,
                Name = dto.Name,
                Description = dto.Description,
                CreatedAt = TimeHelper.GetDateTime(),
                UpdatedAt = TimeHelper.GetDateTime()
            };

            var result = await _repository.CreateAsync(category);
            return result > 0;


        }

        public Task<bool> DeleteAsync(long categoryId)
        {
            throw new NotImplementedException();
        }

        public Task<IList<Category>> GetAllAsync(PaginationParams @params)
        {
            throw new NotImplementedException();
        }

        public Task<Category> GetByIdAsync(long categoryId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(long categoryId, CategoryUpdateDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
