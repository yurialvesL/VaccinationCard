using Microsoft.OpenApi.Models;
using VaccinationCard.Filters;

namespace VaccinationCard.Extensions;

/// <summary>
/// Extension methods for configuring Swagger documentation in the service collection.
/// </summary>
public static class SwaggerDocExtension
{
    public static IServiceCollection AddSwaggerGen(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
            {
                Title = "VaccineCard API",
                Version = "v1",
                Description = "API for Vaccine card application",
                Contact = new Microsoft.OpenApi.Models.OpenApiContact
                {
                    Name = "Support Team",
                    Email = "yurisoad2015@gmail.com"
                }
            });

            c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12sdasdsa3"
            });

            c.OperationFilter<AuthorizeCheckOperationFilter>();

            c.EnableAnnotations();
        });

        return services;
    }
}
