using System;
using Core.Errors;
using Core.Exceptions;
using Core.Interfaces;
using Microsoft.AspNetCore.Diagnostics;
using System.Text.Json;

namespace Web.API.Middleware
{
    /// <summary>
    /// Api exception middleware
    /// </summary>
    public static class ExceptionMiddleware
    {
        public static void ConfigureExceptionHandler(this WebApplication app, ILoggerManager logger, IHostEnvironment env)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.ContentType = "application/json";

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        context.Response.StatusCode = contextFeature.Error switch
                        {
                            NotFoundException => StatusCodes.Status404NotFound,
                            BadRequestException => StatusCodes.Status400BadRequest,
                            _ => StatusCodes.Status500InternalServerError
                        };

                        logger.LogError($"Something went wrong: {contextFeature.Error}");

                        var response = env.IsDevelopment()
                            ? new ApiException(context.Response.StatusCode, contextFeature.Error.Message, contextFeature.Error.StackTrace!.ToString())
                            : new ApiException(context.Response.StatusCode);

                        var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
                        var json = JsonSerializer.Serialize(response, options);

                        await context.Response.WriteAsync(json);
                    }
                });
            });
        }
    }
}

