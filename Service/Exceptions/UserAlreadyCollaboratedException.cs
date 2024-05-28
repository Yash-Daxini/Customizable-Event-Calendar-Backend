namespace Core.Exceptions;

public class UserAlreadyCollaboratedException : Exception
{
    public UserAlreadyCollaboratedException(string message) : base(message)
    {
        
    }
}
