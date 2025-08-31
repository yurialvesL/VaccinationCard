using Microsoft.AspNetCore.Builder;

namespace VaccinationCard.CrossCutting.Common.Middlewares;

/// <summary>
/// Extension methods for configuring the global exception handling middleware in the application builder pipeline.
/// </summary>
public static class GlobalExceptionMiddlewareExtensions
{
    public static IApplicationBuilder UseGlobalExceptionHandler(this IApplicationBuilder app)
        => app.UseMiddleware<GlobalExceptionMiddleware>();
}
