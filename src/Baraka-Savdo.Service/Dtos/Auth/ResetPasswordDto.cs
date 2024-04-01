namespace Baraka_Savdo.Service.Dtos.Auth;

public class ResetPasswordDto
{
    public string NewPassword { get; set; } = string.Empty;
    public string ConfirmPassword { get; set; } = string.Empty;
}
    