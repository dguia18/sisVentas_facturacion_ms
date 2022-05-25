using System.Net;
using SysVentas.Facturation.Application.Base;
namespace SysVentas.Facturacion.WebApi.Infrastructure;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
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
        }
        catch (Exception)
        {
            await HandleExceptionAsync(httpContext);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, SysVentasApplicationException exception)
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
