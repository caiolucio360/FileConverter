using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace FileConverter.Infrastructure.Middleware
{
    public class ApiKeyMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly string _apiKey;
        private const string ApiKeyHeaderName = "x-api-key";

        public ApiKeyMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            _apiKey = configuration["ApiSettings:ApiKey"] ?? string.Empty;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Path.StartsWithSegments("/swagger"))
            {
                await _next(context);
                return;
            }

            if (string.IsNullOrEmpty(_apiKey))
            {
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await context.Response.WriteAsync("API Key não configurada no servidor.");
                return;
            }

            if (!context.Request.Headers.TryGetValue(ApiKeyHeaderName, out var extractedApiKey))
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("API Key é obrigatória.");
                return;
            }

            if (!string.Equals(extractedApiKey, _apiKey))
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized; // Consistente com 401
                await context.Response.WriteAsync("API Key inválida.");
                return;
            }

            await _next(context);
        }
    }
}
