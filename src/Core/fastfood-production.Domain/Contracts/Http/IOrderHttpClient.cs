namespace fastfood_production.Domain.Contracts.Http
{
    public interface IOrderHttpClient
    {
        Task<bool> UpdateOrderStatus(int orderId, CancellationToken cancellationToken, int StatusOrder = 2);
    }
}
