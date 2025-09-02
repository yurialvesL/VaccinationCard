using VaccinationCard.Application;
using VaccinationCard.CrossCutting.Common.Extensions;
using VaccinationCard.CrossCutting.Common.Middlewares;
using VaccinationCard.CrossCutting.Common.Security;
using VaccinationCard.CrossCutting.IoC;
using VaccinationCard.Extensions;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddAppCors(builder.Configuration); // Add CORS configuration

builder.Configuration
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables(); // Load configuration from environment variables

builder.Services.AddControllers(); // Add controllers to the services
builder.Services.AddInfrastructure(builder.Configuration); // Add infrastructure services


builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddMaps(typeof(Program).Assembly, typeof(ApplicationLayer).Assembly);
}); // Add AutoMapper with profiles from specified assemblies


builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssemblies(
        typeof(ApplicationLayer).Assembly,
        typeof(Program).Assembly
    );
});// Add MediatR with handlers from specified assemblies

builder.Services.AddSwaggerGen(); // Add Swagger DOC
builder.Services.AddJwtAuthentication(builder.Configuration); //Add JWT Authentication

var app = builder.Build();

app.UseGlobalExceptionHandler(); // Use global exception handling middleware
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "VaccinationCard.API v1");
    c.RoutePrefix = "";
}); // Configure Swagger UI

app.UseSwagger(); // Enable Swagger middleware


if (app.Environment.IsDevelopment())
{


    app.UseGlobalExceptionHandler();
    app.UseHsts();
}

app.UseHttpsRedirection(); // Redirect HTTP to HTTPS
app.UseAppCors(); // Use CORS policy
app.UseRouting(); // Use routing middleware
app.MapControllers(); // Map controller routes
app.UseAuthentication(); // Use authentication middleware
app.UseAuthorization(); // Use authorization middleware
app.Run(); // Run the application

public partial class Program { }