using fastfood_production.Domain.Contracts.RabbitMq;
using Microsoft.Extensions.Hosting;
using System.Diagnostics.CodeAnalysis;

namespace fastfood_production.Application.UseCases.Consumer;

[ExcludeFromCodeCoverage]
public class RabbitMQConsumerService(IConsumerService rabbitMQConsumer) : BackgroundService
{
    private readonly IConsumerService _rabbitMQConsumer = rabbitMQConsumer;

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        return Task.Run(() =>
        {
            _rabbitMQConsumer.StartConsuming();
            while (!stoppingToken.IsCancellationRequested)
            {
                Task.Delay(100, stoppingToken);
            }
        }, stoppingToken);
    }

    public override void Dispose()
    {
        _rabbitMQConsumer.Dispose();
        base.Dispose();
    }
}