using StackLab.Survey.Domain.Entities;
using StackLab.Survey.Domain.Exceptions;

namespace Domain.UnitTests.Entities;
public class UserTests
{
    [Fact]
    public void Constructor_ValidParameters_ShouldCreateUser()
    {
        var name = "John Doe";
        var email = "johndoe@example.com";
        var password = "securepassword";

        var user = new User(name, email, password);

        Assert.Equal(name, user.Name);
        Assert.Equal(email, user.Email);
    }

    [Fact]
    public void SetName_EmptyName_ShouldThrowValidationException()
    {
        var user = new User("John Doe", "johndoe@example.com", "password");

        var exception = Assert.Throws<ValidationException>(() => user.SetName(""));
        Assert.Equal("Name cannot be null", exception.Message);
    }

    [Fact]
    public void SetEmail_InvalidEmail_ShouldThrowValidationException()
    {
        var user = new User("John Doe", "johndoe@example.com", "password");

        var exception = Assert.Throws<ValidationException>(() => user.SetEmail("invalid-email"));
        Assert.Equal("Invalid Email", exception.Message);
    }

    [Fact]
    public void SetEmail_ValidEmail_ShouldUpdateEmail()
    {
        var user = new User("John Doe", "johndoe@example.com", "password");
        var newEmail = "newemail@example.com";

        user.SetEmail(newEmail);

        Assert.Equal(newEmail, user.Email);
    }

    [Fact]
    public void Constructor_InvalidEmail_ShouldThrowValidationException()
    {
        var name = "John Doe";
        var email = "invalid-email";
        var password = "securepassword";

        var exception = Assert.Throws<ValidationException>(() => new User(name, email, password));
        Assert.Equal("Invalid Email", exception.Message);
    }
}