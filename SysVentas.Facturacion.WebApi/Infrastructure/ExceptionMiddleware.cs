using System.Net;
using FluentValidation;
using SysVentas.Facturation.Application.Base;
using SysVentas.Facturation.Infrastructure.HttpServices.Base;
namespace SysVentas.Facturacion.WebApi.Infrastructure;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger _logger;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (ValidationException exception)
        {
            await HandleExceptionAsync(httpContext, exception);
        }
        catch (SysVentasApplicationException exception)
        {
            await HandleExceptionAsync(httpContext, exception);
        }
        catch (HttpServicesException exception)
        {
            await HandleExceptionAsync(httpContext, exception);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(httpContext);
            _logger.LogError(ex.Message);
        }
    }
    private static Task HandleExceptionAsync(HttpContext context, ValidationException exception)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

        var errors = exception.Errors.Select(error => new
        {
            error.PropertyName,
            error.ErrorMessage
        }).ToList();

        return context.Response.WriteAsync(new ExceptionResponse
        {
            StatusCode = context.Response.StatusCode,
            Message = "There are some validation errors, please check the data and perform the operation again.",
            Errors = errors
        }.ToString());
    }
    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

        return context.Response.WriteAsync(new ExceptionResponse()
        {
            StatusCode = context.Response.StatusCode,
            Message = exception.Message,
        }.ToString());
    }

    private static Task HandleExceptionAsync(HttpContext context)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        return context.Response.WriteAsync(new ExceptionResponse
        {
            StatusCode = context.Response.StatusCode,
            Message = "There was an unexpected error"
        }.ToString());
    }
}
