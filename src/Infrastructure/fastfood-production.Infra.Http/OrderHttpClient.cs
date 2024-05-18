using fastfood_production.Domain.Contracts.Http;
using fastfood_production.Infra.Http.Dto;
using System.Text;
using System.Text.Json;

namespace fastfood_production.Infra.Http;

public class OrderHttpClient : HttpClient, IOrderHttpClient
{
    public OrderHttpClient(string baseAddress)
    {
        BaseAddress = new Uri(baseAddress);
    }

    public async Task<bool> UpdateOrderStatus(int orderId, CancellationToken cancellationToken, int StatusOrder)
    {
        StringContent content = new(JsonSerializer.Serialize(new OrderRequest(orderId, StatusOrder)), Encoding.UTF8, "application/json");

        using HttpResponseMessage response = await PatchAsync(string.Empty, content, cancellationToken);

        return response.IsSuccessStatusCode;
    }
}