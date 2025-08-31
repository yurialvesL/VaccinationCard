using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using VaccinationCard.CrossCutting.Common.Exceptions;

namespace VaccinationCard.CrossCutting.Common.Middlewares;

/// <summary>
/// Global Excep
/// </summary>
public sealed class GlobalExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionMiddleware> _logger;
    private readonly IHostEnvironment _env;

    public GlobalExceptionMiddleware(
        RequestDelegate next,
        ILogger<GlobalExceptionMiddleware> logger,
        IHostEnvironment env)
    {
        _next = next;
        _logger = logger;
        _env = env;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex) when (context.Response.HasStarted is false)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext ctx, Exception ex)
    {
        _logger.LogError(ex, "Unhandled exception: {Path}", ctx.Request.Path);

        ctx.Response.ContentType = "application/problem+json";

        ProblemDetails problem;
        int statusCode;

        switch (ex)
        {
            case ValidationException vex:
                statusCode = StatusCodes.Status400BadRequest;
                var vpd = new ValidationProblemDetails(vex.Errors)
                {
                    Title = "Validation Error",
                    Detail = vex.Message,
                    Status = statusCode,
                    Instance = ctx.Request.Path
                };
                problem = vpd;
                break;

            case UnauthorizedException uex:
                statusCode = StatusCodes.Status401Unauthorized;
                ctx.Response.Headers["WWW-Authenticate"] = "Bearer";
                problem = new ProblemDetails
                {
                    Title = "Not Authenticated",
                    Detail = uex.Message,
                    Status = statusCode,
                    Instance = ctx.Request.Path,
                    Type = "about:blank"
                };
                break;

            case ForbiddenException fex:
                statusCode = StatusCodes.Status403Forbidden;
                problem = new ProblemDetails
                {
                    Title = "Access denied",
                    Detail = fex.Message,
                    Status = statusCode,
                    Instance = ctx.Request.Path,
                    Type = "about:blank"
                };
                break;

            case ConflictException cex:
                statusCode = StatusCodes.Status409Conflict;
                problem = new ProblemDetails
                {
                    Title = "Conflict",
                    Detail = cex.Message,
                    Status = statusCode,
                    Instance = ctx.Request.Path,
                    Type = "about:blank"
                };
                break;

            default:
                statusCode = StatusCodes.Status500InternalServerError;
                problem = new ProblemDetails
                {
                    Title = "Internal error",
                    Detail = _env.IsDevelopment() ? ex.ToString() : "An unexpected error occurred",
                    Status = statusCode,
                    Instance = ctx.Request.Path,
                    Type = "about:blank"
                };
                break;
        }

        ctx.Response.StatusCode = statusCode;

        var json = JsonSerializer.Serialize(
            problem,
            new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = false
            });

        await ctx.Response.WriteAsync(json);
    }
}
