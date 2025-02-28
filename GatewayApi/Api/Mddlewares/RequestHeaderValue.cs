namespace GatewayApi.Api.Middlewares;

public class RequestHeaderValue
{
    private readonly RequestDelegate _next;
    public RequestHeaderValue(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        context.Request.Headers["Gateway-Api"] = "Pass";
        await _next(context);
    }
}