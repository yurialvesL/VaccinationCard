using VaccinationCard.Application;
using VaccinationCard.CrossCutting.Common.Extensions;
using VaccinationCard.CrossCutting.Common.Middlewares;
using VaccinationCard.CrossCutting.Common.Security;
using VaccinationCard.CrossCutting.IoC;
using VaccinationCard.Extensions;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddAppCors(builder.Configuration);

builder.Configuration
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();

builder.Services.AddControllers();
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddMaps(typeof(Program).Assembly, typeof(ApplicationLayer).Assembly);
});


builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssemblies(
        typeof(ApplicationLayer).Assembly,
        typeof(Program).Assembly
    );
});

builder.Services.AddSwaggerGen();
builder.Services.AddJwtAuthentication(builder.Configuration);

var app = builder.Build();

app.UseGlobalExceptionHandler();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "VaccinationCard.API v1");
    c.RoutePrefix = "";
});

app.UseSwagger();


if (app.Environment.IsDevelopment())
{


    app.UseGlobalExceptionHandler();
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseAppCors();
app.UseRouting();
app.MapControllers();
app.UseAuthentication();
app.UseAuthorization();
app.Run();

public partial class Program { }