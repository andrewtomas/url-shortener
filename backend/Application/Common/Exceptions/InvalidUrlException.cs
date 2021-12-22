using System.Runtime.Serialization;

namespace Application.Common.Exceptions;

public class InvalidUrlException : Exception
{
    public InvalidUrlException()
    {
    }

    public InvalidUrlException(string message) : base(message)
    {
    }

    public InvalidUrlException(string message, Exception inner) : base(message, inner)
    {
    }

    protected InvalidUrlException(
        SerializationInfo info,
        StreamingContext context) : base(info, context)
    {
    }
}