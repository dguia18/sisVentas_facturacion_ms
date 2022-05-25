namespace SysVentas.Facturacion.Domain.Services;

public interface IProductService
{
    Task UpdateStock(UpdateStockRequest request);
    public class ApisUrl
    {
        public string ProductsUrl { get; set; }
    }
}
public record UpdateStockRequest(IEnumerable<Items> Products);
public record Items(long ProductId, decimal Quantity);
