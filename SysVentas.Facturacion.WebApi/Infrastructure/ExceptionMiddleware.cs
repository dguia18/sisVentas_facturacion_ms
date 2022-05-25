using System.Net;
using SysVentas.Facturation.Application.Base;
using SysVentas.Facturation.Infrastructure.HttpServices.Base;
namespace SysVentas.Facturacion.WebApi.Infrastructure;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger _logger;

    public ExceptionMiddleware(RequestDelegate next,ILogger<ExceptionMiddleware> logger)
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
        catch (SysVentasApplicationException ex)
        {
            await HandleExceptionAsync(httpContext, ex);
            _logger.LogError(ex.Message);
        }
        catch (HttpServicesException ex)
        {
            await HandleExceptionAsync(httpContext, ex);
            _logger.LogError(ex.Message);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(httpContext);
            _logger.LogError(ex.Message);
        }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
        
        return context.Response.WriteAsync(new
        {
            StatusCode = context.Response.StatusCode,
            Message = exception.Message,
        }.ToString() ?? string.Empty);
    }

    private static Task HandleExceptionAsync(HttpContext context)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        
        return context.Response.WriteAsync(new
        {
            StatusCode = context.Response.StatusCode,
            Message = "There was an unexpected error"
        }.ToString() ?? string.Empty);
    }
}
