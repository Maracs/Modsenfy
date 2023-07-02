using Microsoft.AspNetCore.Authentication;
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
using Microsoft.Extensions.Options;

namespace Modsenfy.BusinessAccessLayer.Services;

public class TokenService
{
    private readonly SymmetricSecurityKey _key;
    private readonly UserRepository _userRepository;
    private readonly IOptions<Extentions.AuthenticationOptions> _authOptions;

    public TokenService(IConfiguration config, UserRepository userRepository, IOptions<Extentions.AuthenticationOptions> authenticationOptions)
    {
        _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationOptions.Value.Key));
        _userRepository = userRepository;
        _authOptions = authenticationOptions;
    }

    private async Task<List<Claim>> GetClaimsAsync(User user)
    {
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.NameId, user.UserId.ToString()),
            new Claim(JwtRegisteredClaimNames.Name, user.UserNickname),
        };

        var role = await _userRepository.GetUserRoleAsync(user);
        claims.Add(new Claim(ClaimTypes.Role, role));

        return claims;
    }

    private SecurityTokenDescriptor GetTokenDescriptor(List<Claim> claims, SigningCredentials creds)
    {
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.Now.AddDays(1),
            SigningCredentials = creds,
            Audience = _authOptions.Value.Audience,
            Issuer = _authOptions.Value.Issuer,
        };
        return tokenDescriptor;
    }

    public async Task<string> GetTokenAsync(User user)
    {
        var claims = await GetClaimsAsync(user);

        var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

        var tokenDescriptor = GetTokenDescriptor(claims, creds);

        var tokenHandler = new JwtSecurityTokenHandler();

        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }
}