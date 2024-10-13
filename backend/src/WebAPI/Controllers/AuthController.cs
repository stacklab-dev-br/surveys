using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StackLab.Survey.Application.Requests.Auth.Commands;
using StackLab.Survey.Application.Requests.Auth.Models;
using StackLab.Survey.Application.Requests.Auth.Queries;
using StackLab.Survey.WebAPI.Common.Controllers;

namespace StackLab.Survey.WebAPI.Controllers;

[Route("auth")]
[Authorize]
public class AuthController : ApiControllerBase
{
    public AuthController() { }

    [HttpPost]
    [Route("login")]
    [AllowAnonymous]
    public async Task<AuthResponse> Login(LoginCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpPost]
    [Route("sing-up")]
    [AllowAnonymous]
    public async Task<AuthResponse> Register(RegisterCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpGet]
    [Route("password/reset/{login}")]
    [AllowAnonymous]
    public async Task ResetPassword([FromRoute] string login)
    {
        await Mediator.Send(new GetResetPasswordCodeQuery { Email = login});
    }

    [HttpPost]
    [Route("password/reset/{login}")]
    [AllowAnonymous]
    public async Task ResetPassword([FromRoute] string login, [FromBody] ResetPasswordCommand command)
    {
        command.SetEmail(login);

        await Mediator.Send(command);
    }


    [HttpGet]
    [Route("verification-code")]
    public async Task GetChangeEmailCode()
    {
        await Mediator.Send(new GetVerificationTokenQuery());
    }

    [HttpPatch]
    [Route("email")]
    public async Task ChangeEmail(ChangeEmailCommand command)
    {
        await Mediator.Send(command);
    }

    [HttpPatch]
    [Route("password")]
    public async Task ChangePassword(ChangePasswordCommand command)
    {
        await Mediator.Send(command);
    }
}

