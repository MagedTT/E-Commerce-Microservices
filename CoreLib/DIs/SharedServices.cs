using CoreLib.DIs;
using CoreLib.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CoreLib.DIs;

public static class SharedServices
{
    public static IServiceCollection SharedService<TContext>(this IServiceCollection services, IConfiguration config)
        where TContext : DbContext
    {
        var constr = config.GetConnectionString("DefaultConnection");
        if (string.IsNullOrWhiteSpace(constr))
            throw new ArgumentException("Connection String Is Missing Or Empty.", nameof(config));

        services.AddDbContext<TContext>(options =>
            options.UseSqlServer(constr, options => options.EnableRetryOnFailure()));

        JwtAuthScheme.AddAuthenticationScheme(services, config);

        return services;
    }

    public static IApplicationBuilder SharedMiddleware(this IApplicationBuilder app)
    {
        app.UseMiddleware<OnlyListenToGateway>();
        return app;
    }
}
