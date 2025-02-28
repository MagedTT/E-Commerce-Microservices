using CoreLib.DIs;
using GatewayApi.Api.Middlewares;
using Ocelot.Cache.CacheManager;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);
builder.Services.AddOcelot().AddCacheManager(x => x.WithDictionaryHandle());
JwtAuthScheme.AddAuthenticationScheme(builder.Services, builder.Configuration);


builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowAnyOrigin();
    });
});

var app = builder.Build();

app.UseCors();

app.UseMiddleware<RequestHeaderValue>();

app.UseOcelot().Wait();

app.UseAuthorization();

app.Run();
