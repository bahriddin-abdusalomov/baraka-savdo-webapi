namespace Baraka_Savdo.Service.Dtos.Categories;

public class CategoryUpdateDto 
{
    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

    public IFormFile? Image { get; set; }
}
