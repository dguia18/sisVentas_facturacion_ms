using System.Runtime.Serialization;
namespace SysVentas.Facturation.Application.Base;

[Serializable]
public class SysVentasApplicationException : Exception
{
    public SysVentasApplicationException() { }
    public SysVentasApplicationException(string message) : base(message) { }
    public SysVentasApplicationException(string message, Exception inner) : base(message, inner) { }
    protected SysVentasApplicationException(
        SerializationInfo info,
        StreamingContext context) : base(info, context)
    {
    }
}
