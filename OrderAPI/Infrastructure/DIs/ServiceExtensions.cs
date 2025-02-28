using CoreLib.DIs;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OrderAPI.Core.Interfaces;
using OrderAPI.Infrastructure.Data;
using OrderAPI.Infrastructure.Repos;

namespace OrderAPI.Infrastructure.DIs;
public static class ServiceExtensions
{
    public static IServiceCollection AddInfrastructureService(this IServiceCollection services, IConfiguration config)
    {
        SharedServices.SharedService<OrderDbContext>(services, config);

        services.AddScoped<IOrder, OrderRepo>();

        return services;
    }

    public static IApplicationBuilder UserInfrastructureMiddleware(this IApplicationBuilder app)
    {
        SharedServices.SharedMiddleware(app);
        return app;
    }
}