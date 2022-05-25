using Infrastructure.Base;
using SysVentas.Facturacion.Domain.Base;
using SysVentas.Facturacion.Domain.Contracts;
using SysVentas.Facturation.Infrastructure.Data.Repositories;
namespace SysVentas.Facturation.Infrastructure.Data.Base;

public class UnitOfWork : IUnitOfWork
{
    private IDbContext _dbContext;
    public UnitOfWork(IDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public IGenericRepository<T> GenericRepository<T>() where T : BaseEntity
    {
            return new GenericRepository<T>(_dbContext);
    }
    public Task<int> CommitAsync()
    {
        return _dbContext.SaveChangesAsync();
    }
    public int Commit()
    {
        return _dbContext.SaveChanges();
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Disposes all external resources.
    /// </summary>
    /// <param name="disposing">The dispose indicator.</param>
    private void Dispose(bool disposing)
    {
        if (!disposing || _dbContext == null) return;
        _dbContext.DoDispose();
        _dbContext = null;
    }
}
