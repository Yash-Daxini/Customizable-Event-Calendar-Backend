namespace Core.Exceptions;

public class EventOverlapException : Exception
{
    public EventOverlapException(string message) : base(message)
    {
        
    }

    public EventOverlapException() : base()
    {
         
    }
}
