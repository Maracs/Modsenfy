﻿using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Modsenfy.DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Modsenfy.BusinessAccessLayer.Extentions;
using Modsenfy.DataAccessLayer.Repositories;

namespace Modsenfy.BusinessAccessLayer.Services;

public class TokenService
{
    private readonly SymmetricSecurityKey _key;
    private readonly UserRepository _userRepository;

    public TokenService(IConfiguration config, UserRepository userRepository)
    {
        _key = Extentions.AuthenticationOptions.GetSymmetricSecurityKey();
        _userRepository = userRepository;
    }

    public async Task<string> GetToken(User user)
    {
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.NameId, user.UserId.ToString()),
            new Claim(JwtRegisteredClaimNames.Name, user.UserNickname),
        };

        var role = await _userRepository.GetUserRole(user);
        claims.Add(new Claim(ClaimTypes.Role, role));

        var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.Now.AddDays(1),
            SigningCredentials = creds,
            Audience = Extentions.AuthenticationOptions.Audience,
            Issuer = Extentions.AuthenticationOptions.Issuer,
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}