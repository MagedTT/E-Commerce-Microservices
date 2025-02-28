using CoreLib.DIs;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProductAPI.Application.Interfaces;
using ProductAPI.Infrastructure.Data;
using ProductApi.Infrastructure.Repos;

namespace ProductApi.Infrastructure.DIs;

public static class ServiceExtensions
{
    public static IServiceCollection AddService(this IServiceCollection services, IConfiguration config)
    {
        SharedServices.SharedService<ProductDbContext>(services, config);
        services.AddScoped<IProduct, ProductRepo>();
        return services;
    }

    public static IApplicationBuilder UseCustomMiddleware(this IApplicationBuilder app)
    {
        SharedServices.SharedMiddleware(app);
        return app;
    }
}