﻿using Baraka_Savdo.DataAccess.Interfaces.Categories;
using Baraka_Savdo.DataAccess.Utils;
using Baraka_Savdo.Domain.Entities.Categories;
using Baraka_Savdo.Domain.Exceptions.Categories;
using Baraka_Savdo.Domain.Exceptions.Files;
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
            return _repository.CountAsync();
        }

        public async Task<bool> CreateAsync(CategoryCreateDto dto)
        {
            string imagePath = await _fileService.UploadImageAsync(dto.ImagePath);
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

        public async Task<bool> DeleteAsync(long categoryId)
        {
            var category =  await _repository.GetByIdAsync(categoryId);
            if (category == null) throw new CategoryNotFoundException();

            var categoryImage = await _fileService.DeleteImageAsync(category.ImagePath);
            if (categoryImage == false) throw new ImageNotFoundException();

            var result = await _repository.DeleteAsync(categoryId);
            return result > 0;
        }

        public async Task<IList<Category>> GetAllAsync(PaginationParams @params)
        {
            var result = await _repository.GetAllAsync(@params);
            return result;
        }

        public async Task<Category> GetByIdAsync(long categoryId) 
        {
            var category = await _repository.GetByIdAsync(categoryId);

            if (category == null) throw new CategoryNotFoundException();
            else return category;
        }

        public async Task<bool> UpdateAsync(long categoryId, CategoryUpdateDto dto)
        {
            var category = await _repository.GetByIdAsync(categoryId);
            if (category is null) throw new CategoryNotFoundException();

            // parse new items to category
            category.Name = dto.Name;
            category.Description = dto.Description;

            if (dto.Image is not null)
            {
                // delete old image
                var deleteResult = await _fileService.DeleteImageAsync(category.ImagePath);
                if (deleteResult is false) throw new ImageNotFoundException();

                // upload new image
                string newImagePath = await _fileService.UploadImageAsync(dto.Image);

                // parse new path to category
                category.ImagePath = newImagePath;
            }
            // else category old image have to save

            category.UpdatedAt = TimeHelper.GetDateTime();

            var dbResult = await _repository.UpdateAsync(categoryId, category);
            return dbResult > 0;

        }
    }
}
