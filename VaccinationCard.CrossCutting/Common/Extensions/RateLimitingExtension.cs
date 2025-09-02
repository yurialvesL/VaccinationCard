using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.RateLimiting;

namespace VaccinationCard.CrossCutting.Common.Extensions;

public static class RateLimitingExtension
{
    public static IServiceCollection AddRateLimitingPolicies(this IServiceCollection services, IConfiguration config)
    {
        var section = config.GetSection("RateLimiting");

        services.AddRateLimiter(options =>
        {
            options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;

            options.AddFixedWindowLimiter("fixed-1m", o =>
            {
                o.PermitLimit = section.GetValue("Fixed:PermitLimit", 100);
                o.Window = TimeSpan.FromSeconds(section.GetValue("Fixed:WindowSeconds", 60));
                o.QueueLimit = 0;
            });

            options.AddPolicy("per-ip", http =>
            {
                var key = http.Connection.RemoteIpAddress?.ToString() ?? "unknown";
                return RateLimitPartition.GetFixedWindowLimiter(key, _ => new FixedWindowRateLimiterOptions
                {
                    PermitLimit = section.GetValue("PerIp:PermitLimit", 20),
                    Window = TimeSpan.FromSeconds(section.GetValue("PerIp:WindowSeconds", 10)),
                    QueueLimit = 0
                });

            });
        });

        return services;
    }
}
