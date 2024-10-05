namespace StackLab.Survey.Application.Exceptions;

public class TooManyRequestsException : Exception
{
    public TooManyRequestsException() : base()
    {
    }

    public TooManyRequestsException(string message) : base(message)
    {
    }
}
