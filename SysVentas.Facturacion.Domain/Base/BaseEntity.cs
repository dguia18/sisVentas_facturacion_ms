namespace SysVentas.Facturacion.Domain.Base;
public abstract class BaseEntity
{
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}
public abstract class Entity<T> : BaseEntity, IEntity<T>
{
    public virtual T Id { get; set; }
}
