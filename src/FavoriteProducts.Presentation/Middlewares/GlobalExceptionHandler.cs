using System.Net.Mime;
using System.Text.Json;
using FavoriteProducts.UseCases.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace FavoriteProducts.Presentation.Middlewares;

public class GlobalExceptionHandler : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception e)
        {
            var details = e switch
            {
                ValidationException validationException => new ProblemDetails
                {
                    Status = StatusCodes.Status400BadRequest,
                    Title = "Validation Error",
                    Detail = "One or more validation errors occurred.",
                    Extensions = { ["errors"] = validationException.Errors }
                },
                _ => new ProblemDetails
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Title = "An error occurred while processing your request.",
                    Detail = e.Message
                }
            };

            context.Response.StatusCode = details.Status ?? StatusCodes.Status500InternalServerError;
            context.Response.ContentType = MediaTypeNames.Application.ProblemJson;
            var problem = JsonSerializer.Serialize(details);

            await context.Response.WriteAsJsonAsync(problem);
        }
    }
}