namespace Baraka_Savdo.Domain.Exceptions.Users;

public class UserAlreadyExistsException : AlreadyExistsException
{
    public UserAlreadyExistsException()
    {
        TitleMessage = "User already !";
    }
}
