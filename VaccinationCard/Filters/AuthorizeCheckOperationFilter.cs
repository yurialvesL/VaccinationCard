using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace VaccinationCard.Filters;

/// <summary>
/// Filter to check for Authorize attribute and add security requirements to Swagger documentation
/// </summary>
public class AuthorizeCheckOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {

        var declaringType = context.MethodInfo.DeclaringType;
        if (declaringType == null)
            return;

        var hasAuthorize = declaringType.GetCustomAttributes(true)
                                .OfType<AuthorizeAttribute>().Any()
                            || context.MethodInfo.GetCustomAttributes(true)
                                .OfType<AuthorizeAttribute>().Any();

        if (!hasAuthorize)
            return;

        operation.Security = new List<OpenApiSecurityRequirement>
            {
                new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                }
            };
    }
}
