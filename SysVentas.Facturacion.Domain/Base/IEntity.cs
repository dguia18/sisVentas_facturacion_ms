namespace SysVentas.Facturacion.Domain.Base;

public interface IEntity<T>
{
    T Id { get; set; }
}
