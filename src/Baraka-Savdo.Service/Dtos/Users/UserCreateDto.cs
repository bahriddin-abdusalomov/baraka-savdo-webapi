using Baraka_Savdo.Domain.Enums;

namespace Baraka_Savdo.Service.Dtos.Users;

public class UserCreateDto
{
    [MaxLength(50)]
    public string FirstName { get; set; } = string.Empty;
    [MaxLength(50)]
    public string LastName { get; set; } = string.Empty;

    public bool? IsMale { get; set; }

    public DateTime? BirthDate { get; set; }

    public string? Country { get; set; } 

    public string? Region { get; set; }

    public IFormFile? ImagePath { get; set; }

    [MaxLength(9)]
    public string? PassportSeriaNumber { get; set; } 
}
