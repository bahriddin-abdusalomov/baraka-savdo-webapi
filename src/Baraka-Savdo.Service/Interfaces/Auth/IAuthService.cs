using Baraka_Savdo.Service.Dtos.Auth;

namespace Baraka_Savdo.Service.Interfaces.Auth;

public interface IAuthService
{
    public Task<bool> RegisterAsync(RegisterDto registerDto);
    public Task<string> LoginAsync(LoginDto loginDto);
    public Task<bool> ResetPasswordAsync(ResetPasswordDto resetPasswordDto);
    public Task<bool> SendVerificationCodeAsync(EmailDto dto);
    public Task<bool> EmailComfirmationCodeAsync(CodeDto dto);
}
