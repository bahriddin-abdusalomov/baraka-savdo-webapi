namespace Baraka_Savdo.Domain.Exceptions.Users;

public class UserNotFoundException : NotFoundException
{
    public UserNotFoundException()
    {
        TitleMessage = "User not found !";
    }
}
