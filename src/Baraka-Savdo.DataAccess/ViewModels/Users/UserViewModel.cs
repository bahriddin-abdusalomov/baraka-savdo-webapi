namespace Baraka_Savdo.DataAccess.ViewModels.Users;

public class UserViewModel
{
    [MaxLength(50)]
    public string FirstName { get; set; } = string.Empty;

    [MaxLength(50)]
    public string LastName { get; set; } = string.Empty;

    public bool IsMale { get; set; }

    public string ImagePath { get; set; } = string.Empty;

    [MaxLength(13)]
    public string PhoneNumber { get; set; } = string.Empty;

    public DateTime LastActivity { get; set; }
}
