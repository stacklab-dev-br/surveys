﻿using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using StackLab.Survey.Application.Common.Interfaces;
using StackLab.Survey.Application.Common.Options;
using StackLab.Survey.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace StackLab.Survey.WebAPI.Services;

public class TokenService : ITokenService
{
    private readonly IOptions<TokenSettings> _settings;

    public TokenService(IOptions<TokenSettings> settings)
    {
        _settings = settings;
    }

    public string GetToken(User user)
    {

        var claims = new List<Claim> {
                    new Claim("id", user.Id.ToString()),
                    new Claim("name", user.Name),
                    new Claim("login", user.Login),
                };

        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.Value.SecurityKey));
        var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

        var tokeOptions = new JwtSecurityToken(
            issuer: _settings.Value.Issuer,
            audience: _settings.Value.Audience,
            claims: claims,
            expires: DateTime.Now.AddHours(_settings.Value.ExpiringTimeInHours),
            signingCredentials: signinCredentials
        );

        var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);

        return tokenString;
    }
}
