using AuthAPI.Core.Interfaces;
using AuthAPI.Infrastructure.Data;
using CoreLib.DIs;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using AuthAPI.Infrastructure.Repos;
using Microsoft.Extensions.DependencyInjection;

namespace AuthAPI.Infrastructure.DIs;

public static class ServiceExtensions
{
    public static IServiceCollection AddInfrastructureService(this IServiceCollection services, IConfiguration config)
    {
        // Add Database Connectivity
        // Add JWT Authentication Scheme
        SharedServices.SharedService<AuthDbContext>(services, config);

        services.AddScoped<IUser, UserRepo>();

        return services;
    }

    public static IApplicationBuilder UseInfrastructureMiddleware(this IApplicationBuilder app)
    {
        SharedServices.SharedMiddleware(app);
        return app;
    }
}