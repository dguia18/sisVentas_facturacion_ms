using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SysVentas.Facturacion.Domain;
using SysVentas.Facturation.Infrastructure.Data.Extensions;
namespace SysVentas.Facturation.Infrastructure.Data.Configuration;

public class InvoiceMasterEntityTypeConfiguration : IEntityTypeConfiguration<InvoiceMaster>
{
    public void Configure(EntityTypeBuilder<InvoiceMaster> builder)
    {
        builder.ToTable(nameof(InvoiceMaster), FacturationDataContext.DefaultSchema);
        builder.HasKey(t => t.Id);
        builder.Property(t => t.Total).CurrencyHasPrecision();
    }
}
public class InvoiceDetailEntityTypeConfiguration : IEntityTypeConfiguration<InvoiceDetail>
{
    public void Configure(EntityTypeBuilder<InvoiceDetail> builder)
    {
        builder.ToTable(nameof(InvoiceDetail), FacturationDataContext.DefaultSchema);
        builder.HasKey(t => t.Id);
        builder.Property(t => t.Quantity).CurrencyHasPrecision();
        builder.Property(t => t.Price).CurrencyHasPrecision();
        builder.Property(t => t.Total).CurrencyHasPrecision();
        
        builder.HasOne(t => t.InvoiceMaster)
            .WithMany(t => t.Details)
            .HasForeignKey(t => t.InvoiceMasterId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
