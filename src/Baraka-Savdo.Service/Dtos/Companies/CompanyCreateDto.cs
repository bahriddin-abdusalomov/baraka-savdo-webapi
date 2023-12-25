namespace Baraka_Savdo.Service.Dtos.Companies;

public class CompanyCreateDto
{
    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public IFormFile ImagePath { get; set; } = default!;

    public string PhoneNumber { get; set; } = string.Empty;
}
