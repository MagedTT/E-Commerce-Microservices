using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OrderAPI.Core.Services;

namespace OrderAPI.Core.DIs;

public static class ServiceContainer
{
    public static IServiceCollection AddCoreService(this IServiceCollection services, IConfiguration config)
    {
        services.AddHttpClient<IOrderService, OrderService>(options =>
        {
            options.BaseAddress = new Uri(config["Gateway-Api:BaseAddress"]!);
            options.Timeout = TimeSpan.FromSeconds(1);
        });
        return services;
    }
}