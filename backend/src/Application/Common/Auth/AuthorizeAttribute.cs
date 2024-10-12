﻿namespace StackLab.Survey.Application.Common.Auth;


[AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
public class AuthorizeAttribute : Attribute
{
    public AuthorizeAttribute() { }
}
