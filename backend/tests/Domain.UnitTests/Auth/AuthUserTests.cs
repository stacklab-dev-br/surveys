using StackLab.Survey.Domain.Auth;
using StackLab.Survey.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.UnitTests.Auth;
public class AuthUserTests
{
    // Classe de teste derivada para testar AuthUser
    private class TestAuthUser : AuthUser { }

    [Fact]
    public void SetLogin_ValidLogin_ShouldSetLogin()
    {
        var authUser = new TestAuthUser();
        var login = "userlogin";

        authUser.SetLogin(login);

        Assert.Equal(login, authUser.Login);
    }

    [Fact]
    public void SetLogin_NullOrEmptyLogin_ShouldThrowValidationException()
    {
        var authUser = new TestAuthUser();

        var exception = Assert.Throws<ValidationException>(() => authUser.SetLogin(null));
        Assert.Equal("Login can not be null", exception.Message);

        exception = Assert.Throws<ValidationException>(() => authUser.SetLogin(""));
        Assert.Equal("Login can not be null", exception.Message);
    }

    [Fact]
    public void SetPassword_ValidPassword_ShouldHashPassword()
    {
        var authUser = new TestAuthUser();
        var password = "strongpassword";

        authUser.SetPassword(password);

        Assert.NotNull(authUser.Password);
        Assert.NotEqual(password, authUser.Password); // Password should be hashed
    }

    [Fact]
    public void SetPassword_NullOrEmptyPassword_ShouldThrowValidationException()
    {
        var authUser = new TestAuthUser();

        var exception = Assert.Throws<ValidationException>(() => authUser.SetPassword(null));
        Assert.Equal("Password can not be null", exception.Message);

        exception = Assert.Throws<ValidationException>(() => authUser.SetPassword(""));
        Assert.Equal("Password can not be null", exception.Message);
    }

    [Fact]
    public void SetPassword_ShortPassword_ShouldThrowValidationException()
    {
        var authUser = new TestAuthUser();
        var shortPassword = "123";

        var exception = Assert.Throws<ValidationException>(() => authUser.SetPassword(shortPassword));
        Assert.Equal("Password must be at least 6 characters long. ", exception.Message);
    }

    [Fact]
    public void VerifyPassword_ValidLoginAndPassword_ShouldReturnTrue()
    {
        var authUser = new TestAuthUser();
        var login = "userlogin";
        var password = "strongpassword";

        authUser.SetLogin(login);
        authUser.SetPassword(password);

        var result = authUser.VerifyPassword(login, password);

        Assert.True(result);
    }

    [Fact]
    public void VerifyPassword_InvalidPassword_ShouldReturnFalse()
    {
        var authUser = new TestAuthUser();
        var login = "userlogin";
        var validPassword = "strongpassword";
        var invalidPassword = "wrongpassword";

        authUser.SetLogin(login);
        authUser.SetPassword(validPassword);

        var result = authUser.VerifyPassword(login, invalidPassword);

        Assert.False(result);
    }

    [Fact]
    public void VerifyPassword_InvalidLogin_ShouldReturnFalse()
    {
        var authUser = new TestAuthUser();
        var validLogin = "userlogin";
        var invalidLogin = "wronglogin";
        var password = "strongpassword";

        authUser.SetLogin(validLogin);
        authUser.SetPassword(password);

        var result = authUser.VerifyPassword(invalidLogin, password);

        Assert.False(result);
    }
}
