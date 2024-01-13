namespace Baraka_Savdo.Domain.Exceptions;

public class AlreadyExistsException : Exception
{
    public string TitleMessage { get; set; } = string.Empty;
}
