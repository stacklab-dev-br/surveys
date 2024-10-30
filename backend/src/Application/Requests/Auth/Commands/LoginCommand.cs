﻿using MediatR;
using Microsoft.EntityFrameworkCore;
using StackLab.Survey.Application.Common.Interfaces;
using StackLab.Survey.Application.Exceptions;
using StackLab.Survey.Application.Requests.Auth.Models;
using StackLab.Survey.Domain.Common.Enums;

namespace StackLab.Survey.Application.Requests.Auth.Commands;
public class LoginCommand : IRequest<AuthResponse>
{
    public string Email { get; set; }
    public string Password { get; set; }
}

public class LoginCommandHandler : IRequestHandler<LoginCommand, AuthResponse>
{
    private readonly IApplicationDbContext _context;
    private readonly ITokenService _tokenService;
    public LoginCommandHandler(IApplicationDbContext context, ITokenService tokenService)
    {
        _context = context;
        _tokenService = tokenService;
    }

    public async Task<AuthResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var email = request.Email.Trim().ToLower().Replace(" ", "");

        var user = await _context.Users
            .Where(x => x.Status == Status.Active)
            .FirstOrDefaultAsync(x => x.Email == email);

        if (user == null)
        {
            throw new UnauthorizedException();
        }

        var result = user.VerifyPassword(user.Email,request.Password);

        if (!result)
        {
            throw new UnauthorizedException();
        }

        return new AuthResponse
        {
            Token = _tokenService.GetToken(user)
        };
    }
}

