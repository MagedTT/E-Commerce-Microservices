using Microsoft.AspNetCore.Http;

namespace CoreLib.Middlewares;

public class OnlyListenToGateway
{
    private readonly RequestDelegate _next;
    public OnlyListenToGateway(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var signedHeader = context.Request.Headers["Gateway-Api"].FirstOrDefault();

        if (signedHeader is null)
        {
            context.Response.StatusCode = StatusCodes.Status503ServiceUnavailable;
            await context.Response.WriteAsync("Service Not Available!");
            return;
        }
        else
            await _next(context);
    }
}