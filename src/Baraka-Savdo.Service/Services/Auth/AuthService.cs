using Baraka_Savdo.DataAccess.Interfaces.Users;
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

        user.FirstName = registerDto.FirstName;
        user.LastName = registerDto.LastName;
        user.PhoneNumber = registerDto.PhoneNumber;
        user.PhoneNumberConfirmed = true;

        user.Salt = Guid.NewGuid().ToString();
        user.PasswordHash = PasswordHasher.HashPassword(registerDto.Password, user.Salt);

        user.CreatedAt = user.UpdatedAt = user.LastActivity = TimeHelper.GetDateTime();
        user.IdentityRole = Domain.Enums.IdentityRole.User;

        var dbResult = await _userRepository.CreateAsync(user);
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

    public Task<(bool result, string token)> ResetPasswordAsync(ResetPasswordDto resetPasswordDto)
    {
        throw new NotImplementedException();
    }
}
