namespace Core.Exceptions;

public class AuthenticationFailedException(string message) : Exception(message)
{
}
