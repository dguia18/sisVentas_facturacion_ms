using System.Text.Json;
using System.Text.Json.Serialization;
namespace SysVentas.Facturacion.WebApi.Infrastructure;

public class ExceptionResponse
{

    public int StatusCode { get; set; }
    public string Message { get; set; }
    public object Errors { get; set; }
    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}
