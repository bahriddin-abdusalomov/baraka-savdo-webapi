using Baraka_Savdo.Domain.Entities.Users;

namespace Baraka_Savdo.Service.Interfaces.Auth;

public interface ITokenService
{
    public string GenerateToken(User user);
}