using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VaccinationCard.CrossCutting.Common.Options;

namespace VaccinationCard.CrossCutting.Common.Extensions;

/// <summary>
/// Cors Extensions to call in program
/// </summary>
public static class CorsExtensions
{
    public static IServiceCollection AddAppCors(this IServiceCollection services, IConfiguration config)
    {
        var settings = config.GetSection("Cors").Get<CorsSettings>() ?? new CorsSettings();

        services.AddSingleton(settings);

        services.AddCors(options =>
        {
            options.AddPolicy(settings.PolicyName, policy =>
            {
                if (settings.AllowedOrigins?.Length > 0)
                    policy.WithOrigins(settings.AllowedOrigins)
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                else
                    policy.AllowAnyOrigin()
                          .AllowAnyHeader()
                          .AllowAnyMethod();

                if (settings.AllowCredentials)
                    policy.AllowCredentials(); 
            });
        });

        return services;
    }

    public static IApplicationBuilder UseAppCors(this IApplicationBuilder app)
    {
        var settings = app.ApplicationServices.GetRequiredService<CorsSettings>();
        app.UseCors(settings.PolicyName);
        return app;
    }
}
