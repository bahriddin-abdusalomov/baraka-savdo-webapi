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

    private async Task<int> VerificationCodeAsync(string email)
    {
        string to, from, pass, mail;

        to = email;
        from = "";
        pass = "";
        mail = RandomVerificationCodeAsync().ToString();

        MailMessage mailMessage = new MailMessage();
        mailMessage.To.Add(to);
        mailMessage.From = new MailAddress(from);
        mailMessage.Subject = "BARAKA SAVDO - Verification Code";

        SmtpClient smtpClient = new SmtpClient("smtp.gmail.com");
        smtpClient.EnableSsl = true;
        smtpClient.Port = 587;
        smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
        smtpClient.Credentials = new NetworkCredential(from, pass);


        return 0;
    }

    private int RandomVerificationCodeAsync()
    {
        var random = new Random();
        int randomNumber = random.Next(10000, 99999);
        return randomNumber;
    }
}
