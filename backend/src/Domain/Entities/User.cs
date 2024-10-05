using StackLab.Survey.Domain.Auth;
using StackLab.Survey.Domain.Exceptions;
using System.Text.RegularExpressions;

namespace StackLab.Survey.Domain.Entities;
public class User : AuthUser
{
    public string Name { get; protected set; }
    public string Email { get; protected set; }

    public User(string name, string email, string password)
    {
        SetName(name);
        SetEmail(email);

        SetLogin(email);
        SetPassword(password);
    }

    public void SetName(string name)
    {
        if (string.IsNullOrEmpty(name)) throw new ValidationException("Name cannot be null");

        Name = name;
    }

    public void SetEmail(string email)
    {
        string emailRegex = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";

        if (!Regex.IsMatch(email, emailRegex))
        {
            throw new ValidationException("Invalid Email");
        }

        Email = email;
    }

}
