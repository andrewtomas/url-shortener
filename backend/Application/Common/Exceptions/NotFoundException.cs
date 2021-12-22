﻿using System.Runtime.Serialization;

namespace Application.Common.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException()
    {
    }

    public NotFoundException(string message) : base(message)
    {
    }

    public NotFoundException(string message, Exception inner) : base(message, inner)
    {
    }

    protected NotFoundException(
        SerializationInfo info,
        StreamingContext context) : base(info, context)
    {
    }
}