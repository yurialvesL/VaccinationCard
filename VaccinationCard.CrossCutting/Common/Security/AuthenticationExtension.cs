using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using VaccinationCard.CrossCutting.Common.Interfaces;
using VaccinationCard.CrossCutting.Common.Options;

namespace VaccinationCard.CrossCutting.Common.Security;


/// <summary>
/// Extension methods for configuring JWT authentication in the service collection.
/// </summary>
public static class AuthenticationExtension
{
    public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration, bool isDev = false)
    {
        services.Configure<JwtOptions>(configuration.GetSection("Jwt"));

        var jwt = configuration.GetSection("Jwt").Get<JwtOptions>();


        var keyBytes = Convert.FromBase64String(jwt.SecretKey);
        var signingKey = new SymmetricSecurityKey(keyBytes);

        services.AddAuthentication(options =>
                 {
                     options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                     options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                 })
                 .AddJwtBearer(options =>
                 {
                     // Em dev, pode desligar HTTPS metadata; em prod, mantenha true
                     options.RequireHttpsMetadata = !isDev;
                     options.SaveToken = true;

                     options.TokenValidationParameters = new TokenValidationParameters
                     {
                         ValidateIssuer = true,
                         ValidIssuer = jwt.Issuer,

                         ValidateAudience = true,
                         ValidAudience = jwt.Audience,

                         ValidateIssuerSigningKey = true,
                         IssuerSigningKey = signingKey,

                         ValidateLifetime = true,
                         ClockSkew = TimeSpan.Zero,
                     };
                 });


        services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
        services.AddScoped<IPasswordHasher, BCryptPasswordHasher>();

        return services;
    }
}
