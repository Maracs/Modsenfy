using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modsenfy.BusinessAccessLayer.Extentions;

public static class AuthenticationExtention
{
    public static IServiceCollection AddIdentityServices
            (this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    
                    IssuerSigningKey = AuthenticationOptions.GetSymmetricSecurityKey(),
                    
                    ValidateIssuer = true,

                    ValidIssuer = AuthenticationOptions.Issuer,

                    ValidateAudience = true,

                    ValidAudience = AuthenticationOptions.Audience,

                    ValidateLifetime = true
                });

        return services;
    }
}

