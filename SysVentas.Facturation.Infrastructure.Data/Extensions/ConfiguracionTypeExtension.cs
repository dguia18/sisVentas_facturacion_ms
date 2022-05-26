using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace SysVentas.Facturation.Infrastructure.Data.Extensions
{
    internal static class ConfiguracionTypeSqlServerExtension
    {
        public static void CurrencyHasPrecision(this PropertyBuilder<decimal> property)
        {
            DecimalHasPrecision(property, 17, 2);
        }
        public static void DecimalHasPrecision(this PropertyBuilder<decimal> property, byte precision, byte scale)
        {
            property.HasColumnType($"decimal({precision}, {scale})");
        }
        public static void PercentHasPrecision(this PropertyBuilder<decimal> property)
        {
            DecimalHasPrecision(property, 5, 4);
        }
    }
}

