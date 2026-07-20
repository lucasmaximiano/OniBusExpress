using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace OnibusExpress.Api.Middlewares;

public class ValidationExceptionMiddleware(
    RequestDelegate next,
    ILogger<ValidationExceptionMiddleware> logger)
{
    private readonly RequestDelegate _next = next;
    private readonly ILogger<ValidationExceptionMiddleware> _logger = logger;

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (ValidationException ex)
        {
            await HandleValidationExceptionAsync(context, ex);
        }
        catch (InvalidOperationException ex)
        {
            await HandleInvalidOperationExceptionAsync(context, ex);
        }
    }

    private async Task HandleValidationExceptionAsync(
        HttpContext context,
        ValidationException exception)
    {
        _logger.LogWarning(
            exception,
            "Erro de validação na requisição {Path}.",
            context.Request.Path);

        var errors = exception.Errors
            .GroupBy(x => x.PropertyName)
            .ToDictionary(
                g => g.Key,
                g => g.Select(x => x.ErrorMessage).ToArray());

        var firstError = exception.Errors.First();

        ValidationProblemDetails problemDetails = new(errors)
        {
            Type = "https://tools.ietf.org/html/rfc9110#section-15.5.1",
            Title = "Um ou mais erros de validação ocorreram.",
            Status = StatusCodes.Status400BadRequest,
            Detail = "Verifique os campos informados.",
            Instance = context.Request.Path
        };

        problemDetails.Extensions["field"] = firstError.PropertyName;
        problemDetails.Extensions["message"] = firstError.ErrorMessage;

        await WriteProblemDetailsAsync(context, problemDetails);
    }

    private async Task HandleInvalidOperationExceptionAsync(
        HttpContext context,
        InvalidOperationException exception)
    {
        _logger.LogWarning(
            exception,
            "Operação inválida na requisição {Path}.",
            context.Request.Path);

        ProblemDetails problemDetails = new()
        {
            Type = "https://tools.ietf.org/html/rfc9110#section-15.5.1",
            Title = "Operação inválida.",
            Status = StatusCodes.Status400BadRequest,
            Detail = exception.Message,
            Instance = context.Request.Path
        };

        await WriteProblemDetailsAsync(context, problemDetails);
    }

    private static async Task WriteProblemDetailsAsync(
        HttpContext context,
        ProblemDetails problemDetails)
    {
        context.Response.StatusCode =
            problemDetails.Status ?? StatusCodes.Status500InternalServerError;

        context.Response.ContentType = "application/problem+json";

        JsonSerializerOptions options = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        await context.Response.WriteAsync(
            JsonSerializer.Serialize(problemDetails, options));
    }
}