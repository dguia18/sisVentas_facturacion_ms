using Microsoft.Extensions.Options;
using SysVentas.Facturacion.Domain.Services;
using SysVentas.Facturation.Infrastructure.HttpServices.Base;
namespace SysVentas.Facturation.Infrastructure.HttpServices;

public class ProductService : IProductService
{
    private readonly HttpClient _httpClient;
    private readonly IProductService.ApisUrl _apiUrls;
    public ProductService(HttpClient httpClient, IOptions<IProductService.ApisUrl> apiUrls)
    {
        _httpClient = httpClient;
        _apiUrls = apiUrls.Value;
    }
    public async Task UpdateStock(UpdateStockRequest request)
    {
        var response = await _httpClient.PutHttpAsync(_apiUrls.ProductsUrl + "stocks", request);
        if (!response.IsSuccessStatusCode)
        {
            throw new HttpServicesException($"{response.RequestMessage}");
        }
    }
}
