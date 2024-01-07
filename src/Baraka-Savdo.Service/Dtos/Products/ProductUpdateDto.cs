namespace Baraka_Savdo.Service.Dtos.Products;

public class ProductUpdateDto
{
    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public IFormFile ImagePath { get; set; }

    public double UnitPrice { get; set; }

    public long CategoryId { get; set; }

    public long CompanyId { get; set; }
}
