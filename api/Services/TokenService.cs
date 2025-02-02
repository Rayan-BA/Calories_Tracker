﻿#pragma warning disable CS8604

using api.Interfaces;
using api.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace api.Services;

public class TokenService : ITokenService
{
  private readonly IConfiguration _configuration;
  private readonly SymmetricSecurityKey _key;

  public TokenService(IConfiguration configuration)
  {
    _configuration = configuration;
    _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:SingingKey"]));
  }

  public string CreateToken(User user)
  {
    var claims = new List<Claim>
    {
      new(JwtRegisteredClaimNames.GivenName, user.UserName),
      new(JwtRegisteredClaimNames.Email, user.Email)
    };
    var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);
    var tokenDescriptor = new SecurityTokenDescriptor
    {
      Subject = new ClaimsIdentity(claims),
      Expires = DateTime.Now.AddDays(7),
      SigningCredentials = creds,
      Issuer = _configuration["JWT:Issuer"],
      Audience = _configuration["JWT:Audience"]
    };

    var tokenHandler = new JwtSecurityTokenHandler();
    var token = tokenHandler.CreateToken(tokenDescriptor);
    return tokenHandler.WriteToken(token);
  }
}
