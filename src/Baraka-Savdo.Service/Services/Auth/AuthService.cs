using Baraka_Savdo.DataAccess.Interfaces.Users;
using Baraka_Savdo.Domain.Entities.Users;
using Baraka_Savdo.Domain.Exceptions;
using Baraka_Savdo.Domain.Exceptions.Password;
using Baraka_Savdo.Domain.Exceptions.Users;
using Baraka_Savdo.Service.Common.Helpers;
using Baraka_Savdo.Service.Common.Security;
using Baraka_Savdo.Service.Dtos.Auth;
using Baraka_Savdo.Service.Interfaces.Auth;

using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

using System.Net;
using System.Net.Mail;

namespace Baraka_Savdo.Service.Services.Auth;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly ITokenService _tokenService;
    private readonly IConfiguration _configuration;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IMemoryCache _memoryCache;
    private readonly int MIN_VERIFICATION_CODE = 100000;

    public AuthService(
        IUserRepository userRepository,
        ITokenService tokenService,
        IConfiguration configuration,
        IMemoryCache memoryCache,
        IHttpContextAccessor httpContextAccessor)
    {
        this._userRepository = userRepository;
        this._tokenService = tokenService;
        this._configuration = configuration;
        _memoryCache = memoryCache;
        _httpContextAccessor = httpContextAccessor;
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
        var id = _httpContextAccessor.HttpContext.Request.Cookies["id"];
       
        var user = await _userRepository.GetByIdAsync(long.Parse(id));
        if (user is null) throw new UserNotFoundException();

        string salt = Guid.NewGuid().ToString();
        if (resetPasswordDto.NewPassword == resetPasswordDto.ConfirmPassword)
        {
            user.Salt = salt;
            user.PasswordHash = PasswordHasher.HashPassword(resetPasswordDto.NewPassword, salt);
        }

        var dbResult = await _userRepository.UpdateAsync(user.Id, user);
        return dbResult > 0;
    }

    public async Task<bool> SendVerificationCodeAsync(EmailDto dto)
    {
        var user = await _userRepository.GetByEmailAsync(dto.Email);
        if (user is null) throw new UserNotFoundException();

        //  _httpContextAccessor.HttpContext.Response.Cookies.Append("id", user.Id.ToString(), new CookieOptions { HttpOnly = true, SameSite = SameSiteMode.None, Secure = true, MaxAge = TimeSpan.FromMinutes(5) });

        _httpContextAccessor.HttpContext.Response.Cookies.Append("UserId", user.Id.ToString());
        int code = new Random().Next(MIN_VERIFICATION_CODE, 999999);

        _memoryCache.Set(code,dto.Email);
        var eemail = _memoryCache.Get(code) as string;
        await SendEmailAsync(dto.Email, code);
        
        return true;
    }

    public async Task<bool> EmailComfirmationCodeAsync(CodeDto dto)
    {
        var email = _memoryCache.Get(dto.Code) as string;
       
        if(email.IsNullOrEmpty()) throw new NotFoundException();
        return true;
    }

    private async Task SendEmailAsync(string email, int code)
    {
        var emailSettings = _configuration.GetSection("EmailSettings");
        var mailMessage = new MailMessage
        {
            From = new MailAddress(emailSettings["Sender"], emailSettings["SenderName"]),
            Subject = "Verification Code",
            Body = $"Your verification code is {code}",
            IsBodyHtml = true,
        };

        mailMessage.To.Add(email);

        using var smtp = new SmtpClient(emailSettings["MailServer"], int.Parse(emailSettings["MailPort"]))
        {
            Port = int.Parse(emailSettings["MailPort"]),
            DeliveryMethod = SmtpDeliveryMethod.Network,
            Credentials = new NetworkCredential(emailSettings["Sender"], emailSettings["Password"]),
            EnableSsl = true,
        };

        await smtp.SendMailAsync(mailMessage);
    }

}
