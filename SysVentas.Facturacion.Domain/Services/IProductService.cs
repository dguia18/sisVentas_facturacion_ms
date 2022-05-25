namespace SysVentas.Facturacion.Domain.Services;

public interface IProductService
{
    Task UpdateStock(UpdateStockRequest request);
    public record ApisUrl(string ProductsUrl);
}
public record UpdateStockRequest(IEnumerable<Items> Products);
public record Items(long ProductId,decimal Quantity);
