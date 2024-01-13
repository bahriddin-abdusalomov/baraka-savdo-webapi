namespace Baraka_Savdo.Service.Dtos.Categories;

public class CategoryCreateDto 
{
    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; } 

    public IFormFile ImagePath { get; set; } = default!;
}
