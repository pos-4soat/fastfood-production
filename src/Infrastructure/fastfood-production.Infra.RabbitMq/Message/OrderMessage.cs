using System.Diagnostics.CodeAnalysis;

namespace fastfood_production.Infra.RabbitMq.Message;

[ExcludeFromCodeCoverage]
public class OrderMessage(int orderId, int status)
{
    public int Id { get; set; } = orderId;
    public int Status { get; set; } = status;
}
