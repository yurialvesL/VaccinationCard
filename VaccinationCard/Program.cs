using Microsoft.AspNetCore.Diagnostics;
using VaccinationCard.Application;
using VaccinationCard.CrossCutting.Common.Middlewares;
using VaccinationCard.CrossCutting.Common.Security;
using VaccinationCard.CrossCutting.IoC;
using VaccinationCard.Extensions;

var builder = WebApplication.CreateBuilder(args);


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

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{


    app.UseGlobalExceptionHandler();
    //app.UseExceptionHandler(errorApp =>
    //{
    //    errorApp.Run(async context =>
    //    {
    //        context.Response.StatusCode = 500;
    //        context.Response.ContentType = "application/json";

    //        var errorFeature = context.Features.Get<IExceptionHandlerFeature>();
    //        var exception = errorFeature?.Error;

    //        var logger = context.RequestServices.GetRequiredService<ILogger<Program>>();
    //        logger.LogError(exception, "Unhandled exception");

    //        await context.Response.WriteAsJsonAsync(new
    //        {
    //            error = "Ocorreu um erro interno. Tente novamente mais tarde."
    //        });
    //    });
    //});
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();
app.MapControllers();
app.UseAuthentication();
app.UseAuthorization();

app.Run();


public partial class Program { }