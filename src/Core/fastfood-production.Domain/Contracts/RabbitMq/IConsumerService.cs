namespace fastfood_production.Domain.Contracts.RabbitMq;

public interface IConsumerService
{
    void PublishOrder(int orderId, int status);
    void StartConsuming();
    void Dispose();
}
