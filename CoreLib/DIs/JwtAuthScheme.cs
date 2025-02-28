using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace CoreLib.DIs;

public static class JwtAuthScheme
{
    public static IServiceCollection AddAuthenticationScheme(this IServiceCollection services, IConfiguration config)
    {
        services.AddAuthentication()
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = config.GetSection("Jwt:Issuer").Value!,
                    ValidateAudience = true,
                    ValidAudience = config.GetSection("Jwt:Audience").Value!,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.GetSection("Jwt:SigningKey").Value!))
                };
            });

        return services;
    }
}