using System.Text;
using System.Text.Json;
namespace SysVentas.Facturation.Infrastructure.HttpServices.Base;

public static class HttpClientExtension
{
    public static Task<HttpResponseMessage> PutHttpAsync(this HttpClient httpClient, string requestUri, object request)
    {
        var content = new StringContent(
            JsonSerializer.Serialize(request),
            Encoding.UTF8,
            "application/json"
        );
        return httpClient.PutAsync(requestUri, content);
    }
}
