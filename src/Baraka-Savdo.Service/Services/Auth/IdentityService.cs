using Baraka_Savdo.Domain.Enums;
using Baraka_Savdo.Service.Interfaces.Auth;

namespace Baraka_Savdo.Service.Services.Auth;

public class IdentityService : IIdentityService
{
    private readonly IHttpContextAccessor _accessor;
    public IdentityService(IHttpContextAccessor accessor)
    {
        _accessor = accessor;
    }

    public long UserId
    {
        get
        {
            if (_accessor.HttpContext is null) return 0;
            var claim = _accessor.HttpContext.User.Claims.FirstOrDefault(op => op.Type == "Id");
            if (claim is null) return 0;
            else return long.Parse(claim.Value);
        }
    }

    public string FirstName
    {
        get
        {
            if (_accessor.HttpContext is null) return "";
            var claim = _accessor.HttpContext.User.Claims.FirstOrDefault(op => op.Type == "FirstName");
            if (claim is null) return "";
            else return claim.Value;
        }
    }

    public string LastName
    {
        get
        {
            if (_accessor.HttpContext is null) return "";
            var claim = _accessor.HttpContext.User.Claims.FirstOrDefault(op => op.Type == "Lastname");
            if (claim is null) return "";
            else return claim.Value;
        }
    }

    public string PhoneNumber
    {
        get
        {
            if (_accessor.HttpContext is null) return "";
            string type = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/mobilephone";
            var claim = _accessor.HttpContext.User.Claims.FirstOrDefault(op => op.Type == type);
            if (claim is null) return "";
            else return claim.Value;
        }
    }

    public IdentityRole? IdentityRole
    {
        get
        {
            if (_accessor.HttpContext is null) return null;
            string type = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role";
            var claim = _accessor.HttpContext.User.Claims.FirstOrDefault(op => op.Type == type);
            if (claim is null) return null;
            else
            {
                if (Enum.TryParse(claim.Value, out IdentityRole role))
                {
                    return role;
                }
                else return null;
            }
        }
    }
}
