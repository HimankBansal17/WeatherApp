using System.Net;

namespace WeatherWorksService.Infrastructure.Middlewares
{
    public class ApiKeyAuthMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;
        public ApiKeyAuthMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            _configuration = configuration;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (!context.Request.Headers.TryGetValue("x-api-key", out var apiKey))
            {
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                await context.Response.WriteAsync("Unauthorised Request: Api Key Missing");
                return;
            }

            var requiredApiKey = _configuration.GetSection("Authentication:ApiKey").Value;

            if (!apiKey.ToString().Trim().Equals(requiredApiKey.Trim(), StringComparison.OrdinalIgnoreCase))
            {

                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                await context.Response.WriteAsync("Unauthorised Request: Incorrect Api key");
                return;
            }

            await _next(context);
        }
    }
}
