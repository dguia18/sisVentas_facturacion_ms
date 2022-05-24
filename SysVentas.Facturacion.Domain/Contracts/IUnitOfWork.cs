using SysVentas.Facturacion.Domain.Base;
namespace SysVentas.Facturacion.Domain.Contracts;

public interface IUnitOfWork : IDisposable
{
    IGenericRepository<T> GenericRepository<T>() where T : BaseEntity;
    int Commit();
    Task<int> CommitAsync();
}
