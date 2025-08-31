﻿using Microsoft.AspNetCore.Builder;

namespace VaccinationCard.CrossCutting.Common.Middlewares;

public static class GlobalExceptionMiddlewareExtensions
{
    public static IApplicationBuilder UseGlobalExceptionHandler(this IApplicationBuilder app)
        => app.UseMiddleware<GlobalExceptionMiddleware>();
}
