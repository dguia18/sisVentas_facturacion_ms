using Microsoft.EntityFrameworkCore;
using SysVentas.Facturacion.Domain;
using SysVentas.Facturation.Infrastructure.Data.Base;
using SysVentas.Facturation.Infrastructure.Data.Configuration;
namespace SysVentas.Facturation.Infrastructure.Data;

public class FacturationDataContext : DbContextBase
{
    public DbSet<InvoiceMaster> InvoicesMasters { get; set; }
    public DbSet<InvoiceDetail> InvoicesDetails { get; set; }
    public static string DefaultSchema => "Facturation";
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new InvoiceMasterEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new InvoiceDetailEntityTypeConfiguration());
    }
    public FacturationDataContext(DbContextOptions options) : base(options)
    {
    }
}