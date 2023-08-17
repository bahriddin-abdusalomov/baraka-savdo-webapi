using Baraka_Savdo.Domain.Entities.Categories;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baraka_Savdo.Service.Dtos.Categories
{
    public class CategoryCreateDto 
    {
        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public IFormFile ImagePath { get; set; } = default!;
    }
}
