using System.Runtime.Serialization;
namespace SysVentas.Facturation.Infrastructure.HttpServices.Base;

[Serializable]
public class HttpServicesException : Exception
{
    public HttpServicesException() { }
    public HttpServicesException(string message) : base(message) { }
    public HttpServicesException(string message, Exception inner) : base(message, inner) { }
    protected HttpServicesException(
        SerializationInfo info,
        StreamingContext context) : base(info, context)
    {
    }
}