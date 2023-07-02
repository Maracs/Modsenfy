using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
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
        services.Configure<AuthenticationOptions>(configuration.GetSection("AuthenticationOptions"));
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                 var serviceProvider = services.BuildServiceProvider();
                 var authenticationOptions = serviceProvider.GetRequiredService<IOptions<AuthenticationOptions>>().Value;

                 options.TokenValidationParameters = new TokenValidationParameters
                 {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationOptions.Key)),
                    ValidateIssuer = true,
                    ValidIssuer = authenticationOptions.Issuer,
                    ValidateAudience = true,
                    ValidAudience = authenticationOptions.Audience,
                    ValidateLifetime = true
                 };
        });

        return services;
    }
}

