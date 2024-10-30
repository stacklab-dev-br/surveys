﻿using MediatR;
using Microsoft.EntityFrameworkCore;
using StackLab.Survey.Application.Common.Interfaces;
using StackLab.Survey.Application.Exceptions;
using StackLab.Survey.Application.Requests.Auth.Models;
using StackLab.Survey.Domain.Entities;

namespace StackLab.Survey.Application.Requests.Auth.Commands;
public class RegisterCommand : IRequest<AuthResponse>
{
    public string Name { get; set; }

    public string Email { get; set; }
    public string Password { get; set; }
}

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, AuthResponse>
{
    private readonly IApplicationDbContext _context;
    private readonly ITokenService _tokenService;
    public RegisterCommandHandler(IApplicationDbContext context, ITokenService tokenService)
    {
        _context = context;
        _tokenService = tokenService;
    }

    public async Task<AuthResponse> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var email = request.Email.ToLower().Trim().Replace(" ", "");

            var emailExists = await _context.Users.AnyAsync(x => x.Email ==  email);

            if (emailExists)
            {
                throw new BadRequestException("Login already exists");
            }

            var user = new User(request.Name, email);

            user.SetPassword(request.Password);

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync(cancellationToken);

            return new AuthResponse
            {
                Token = _tokenService.GetToken(user)
            };
        }
        catch (Exception ex)
        {
            throw new BadRequestException(ex.Message);
        }
    }
}

