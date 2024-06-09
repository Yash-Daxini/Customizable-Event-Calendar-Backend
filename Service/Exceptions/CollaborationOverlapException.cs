namespace Core.Exceptions;

public class CollaborationOverlapException : Exception
{
    public CollaborationOverlapException(string message) : base(message)
    {
        
    }

    public CollaborationOverlapException() : base()
    {
        
    }
}
