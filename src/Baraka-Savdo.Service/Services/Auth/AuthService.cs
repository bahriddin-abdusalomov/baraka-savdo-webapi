using Baraka_Savdo.DataAccess.Interfaces.Users;
using Baraka_Savdo.Domain.Entities.Users;
using Baraka_Savdo.Domain.Exceptions.Password;
using Baraka_Savdo.Domain.Exceptions.Users;
using Baraka_Savdo.Service.Common.Helpers;
using Baraka_Savdo.Service.Common.Security;
using Baraka_Savdo.Service.Dtos.Auth;
using Baraka_Savdo.Service.Interfaces.Auth;

using System.Net;
using System.Net.Mail;

namespace Baraka_Savdo.Service.Services.Auth;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly ITokenService _tokenService;
    private readonly int MIN_VERIFICATION_CODE = 1000;

    public AuthService(
        IUserRepository userRepository,
        ITokenService tokenService)
    {
        this._userRepository = userRepository;
        this._tokenService = tokenService;
    }

    public async Task<bool> RegisterAsync(RegisterDto registerDto)
    {
        var user = await _userRepository.GetByEmailAsync(registerDto.Email);
        if (user is not null) throw new UserAlreadyExistsException();

        string salt = Guid.NewGuid().ToString();

        user = new User
        {
            FirstName = registerDto.FirstName,
            LastName = registerDto.LastName,
            Email = registerDto.Email,

            Salt = salt,
            PasswordHash = PasswordHasher.HashPassword(registerDto.Password, salt),

            CreatedAt = TimeHelper.GetDateTime(),
            UpdatedAt = TimeHelper.GetDateTime(),
            LastActivity = TimeHelper.GetDateTime(),
            IdentityRole = Domain.Enums.IdentityRole.Admin,
        };

        var dbResult = await _userRepository.CreateAsync(user);
        return dbResult > 0;
    }

    public async Task<string> LoginAsync(LoginDto loginDto)
    {
        var user = await _userRepository.GetByEmailAsync(loginDto.Email);
        if (user is null) throw new UserNotFoundException();

        var hasherResult = PasswordHasher.VerifyPassword(loginDto.Password, user.Salt, user.PasswordHash);
        if (hasherResult == false) throw new WrongPasswordException();

        string token = _tokenService.GenerateToken(user);
        return token;
    }

    public async Task<bool> ResetPasswordAsync(ResetPasswordDto resetPasswordDto)
    {
        var user = await _userRepository.GetByEmailAsync(resetPasswordDto.Email);
        if (user is null) throw new UserNotFoundException();

        string salt = Guid.NewGuid().ToString();

        user.Salt = salt;
        user.PasswordHash = PasswordHasher.HashPassword(resetPasswordDto.Password, salt);

        var dbResult = await _userRepository.UpdateAsync(user.Id, user);
        return dbResult > 0;
    }

    public async Task<bool> SendVerificationCodeAsync(string email)
    {
        var user = await _userRepository.GetByEmailAsync(email);
        if (user is null) throw new UserNotFoundException();

        int code = new Random().Next(MIN_VERIFICATION_CODE, 9999);

        var dbResult = await _userRepository.UpdateAsync(user.Id, user);
        if (dbResult > 0)
        {
            await SendEmailAsync(email, code);
            return true;
        }
        return false;
    }

    private async Task SendEmailAsync(string email, int code)
    {
        var fromAddress = new MailAddress("bahriddinabdusalomov7@gmail.com");
        var toAddress = new MailAddress(email);
        const string fromPassword = "bahriddin6669";
        const string subject = "Verification Code";
        string body = $"Your verification code is {code}";

        var smtp = new SmtpClient
        {
            Host = "smtp.gmail.com",
            Port = 587,
            EnableSsl = true,
            DeliveryMethod = SmtpDeliveryMethod.Network,
            UseDefaultCredentials = false,
            Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
        };

        smtp.EnableSsl = false;
        
        using var message = new MailMessage(fromAddress, toAddress)
        {
            Subject = subject,
            Body = body
        };
        await smtp.SendMailAsync(message);
    }

}
