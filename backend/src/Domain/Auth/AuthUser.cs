using Microsoft.AspNetCore.Identity;
using StackLab.Survey.Domain.Common.Entities;
using StackLab.Survey.Domain.Exceptions;

namespace StackLab.Survey.Domain.Auth;
public abstract class AuthUser : BaseEntity
{
    public string Login { get; protected set; }
    public string Password { get; protected set; }

    public void SetLogin(string login)
    {
        if (string.IsNullOrEmpty(login))
        {
            throw new ValidationException("Login can not be null");
        }

        Login = login;
    }

    public void SetPassword(string password)
    {
        if (string.IsNullOrEmpty(password))
        {
            throw new ValidationException("Password can not be null");
        }

        if (password.Length < 6)
        {
            throw new ValidationException("Password must be at least 6 characters long. ");
        }

        var passwordHasher = new PasswordHasher<AuthUser>();
        Password = passwordHasher.HashPassword(this, password);
    }

    public bool VerifyPassword(string login, string password)
    {
        var passwordHasher = new PasswordHasher<AuthUser>();
        var result = passwordHasher.VerifyHashedPassword(this, Password, password);

        return login == Login && result == PasswordVerificationResult.Success;
    }


}
