using Baraka_Savdo.DataAccess.Interfaces.Users;
using Baraka_Savdo.Domain.Entities.Users;
using Baraka_Savdo.Domain.Exceptions.Password;
using Baraka_Savdo.Domain.Exceptions.Users;
using Baraka_Savdo.Service.Common.Helpers;
using Baraka_Savdo.Service.Common.Security;
using Baraka_Savdo.Service.Dtos.Auth;
using Baraka_Savdo.Service.Interfaces.Auth;

namespace Baraka_Savdo.Service.Services.Auth;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly ITokenService _tokenService;

    public AuthService(
        IUserRepository userRepository,
        ITokenService tokenService)
    {
        this._userRepository = userRepository;
        this._tokenService = tokenService;
    }

    public async Task<bool> RegisterAsync(RegisterDto registerDto)
    {
        var user = await _userRepository.GetByPhoneAsync(registerDto.PhoneNumber);
        if (user is not null) throw new UserAlreadyExistsException();

        string salt = Guid.NewGuid().ToString();

        User registerUser = new User
        {
            FirstName = registerDto.FirstName,
            LastName = registerDto.LastName,
            PhoneNumber = registerDto.PhoneNumber,
            PhoneNumberConfirmed = true,

            Salt = salt,
            PasswordHash = PasswordHasher.HashPassword(registerDto.Password, salt),

            CreatedAt = TimeHelper.GetDateTime(),
            UpdatedAt = TimeHelper.GetDateTime(),
            LastActivity = TimeHelper.GetDateTime(),
            IdentityRole = Domain.Enums.IdentityRole.User,
    };

        var dbResult = await _userRepository.CreateAsync(registerUser);
        return dbResult > 0;
    }

    public async Task<(bool result, string token)> LoginAsync(LoginDto loginDto)
    {
        var user = await _userRepository.GetByPhoneAsync(loginDto.PhoneNumber);
        if (user is null) throw new UserNotFoundException();

        var hasherResult = PasswordHasher.VerifyPassword(loginDto.Password, user.Salt, user.PasswordHash);
        if (hasherResult == false) throw new WrongPasswordException();

        string token = _tokenService.GenerateToken(user);
        return (result: true, token: token);
    }

    public async Task<(bool result, string token)> ResetPasswordAsync(ResetPasswordDto resetPasswordDto)
    {
        //var user = await _userRepository.GetByPhoneAsync(resetPasswordDto.PhoneNumber);
        // T

        throw new NotImplementedException();
    }
}
