﻿using Microsoft.AspNetCore.Identity;
using StackLab.Survey.Domain.Common.Entities;
using StackLab.Survey.Domain.Common.Enums;
using StackLab.Survey.Domain.Exceptions;
using System.Text.RegularExpressions;

namespace StackLab.Survey.Domain.Entities;

public class User : BaseEntity
{
    public string Name { get; protected set; }
    public Status Status { get; protected set; }

    public string Email { get; protected set; }
    public string Password { get; protected set; }

    public IList<VerificationToken> VerificationTokens { get; protected set; } = new List<VerificationToken>();

    private static readonly PasswordHasher<User> PasswordHasher = new PasswordHasher<User>();

    public User(string name, string email)
    {
        SetName(name);
        SetEmail(email);

        Status = Status.Active;
    }

    public void SetName(string name)
    {
        if (string.IsNullOrEmpty(name)) throw new DomainValidationException("Name cannot be null");

        Name = name;
    }

    public void SetEmail(string email)
    {
        string emailRegex = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";

        if (!Regex.IsMatch(email, emailRegex))
        {
            throw new DomainValidationException("Invalid Email");
        }

        Email = email;
    }

    public void SetPassword(string password)
    {
        if (string.IsNullOrEmpty(password))
        {
            throw new DomainValidationException("Password cannot be null");
        }

        if (password.Length < 6)
        {
            throw new DomainValidationException("Password must be at least 6 characters long ");
        }

        Password = PasswordHasher.HashPassword(this, password);
    }

    public bool VerifyPassword(string email, string password)
    {
        var result = PasswordHasher.VerifyHashedPassword(this, Password, password);
        return email == Email && result == PasswordVerificationResult.Success;
    }

    public VerificationToken GetVerificationToken()
    {
        var token = VerificationTokens.FirstOrDefault(x => !x.IsExpired);

        if (token == null)
        {
            token = new VerificationToken();
            VerificationTokens.Add(token);
        }
        else
        {
            token.ResetExpiration();
        }

        return token;
    }

    public bool ValidateToken(string token)
    {
        return VerificationTokens.Any(x => x.Validate(token));
    }

    public void ClearVerificationTokens(bool all = false)
    {
        if (all)
        {
            VerificationTokens.Clear();
        }
        else
        {
            var tokens = VerificationTokens.ToList();

            foreach (var token in tokens)
            {
                if (token.IsExpired)
                {
                    VerificationTokens.Remove(token);
                }
            }
        }
    }
}
