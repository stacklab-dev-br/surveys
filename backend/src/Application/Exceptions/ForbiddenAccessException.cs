﻿namespace StackLab.Survey.Application.Exceptions;
public class ForbiddenAccessException : Exception
{
    public ForbiddenAccessException() : base()
    {
    }

    public ForbiddenAccessException(string message) : base(message)
    {
    }
}
