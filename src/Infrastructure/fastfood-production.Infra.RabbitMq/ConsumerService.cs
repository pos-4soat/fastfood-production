using fastfood_production.Application.UseCases.CreateProduction;
using fastfood_production.Domain.Contracts.RabbitMq;
using fastfood_production.Infra.RabbitMq.Message;
using fastfood_production.Infra.RabbitMq.Settings;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Text.Json;

namespace fastfood_production.Infra.RabbitMq;

[ExcludeFromCodeCoverage]
public class ConsumerService : IConsumerService, IDisposable
{
    private readonly RabbitMqSettings _settings;
    private IConnection _connection;
    private IModel _channel;
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public ConsumerService(IOptions<RabbitMqSettings> options, IServiceScopeFactory serviceScopeFactory)
    {
        _settings = options.Value;
        _serviceScopeFactory = serviceScopeFactory;
        InitializeRabbitMQ();
    }

    private void InitializeRabbitMQ()
    {
        ConnectionFactory factory = new()
        {
            HostName = _settings.HostName,
            UserName = _settings.UserName,
            Password = _settings.Password,
            Port = 5671,
            Ssl = new SslOption
            {
                Enabled = true,
                ServerName = _settings.HostName
            }
        };

        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();

        _channel.QueueDeclare(queue: _settings.OrderQueueName,
                              durable: true,
                              exclusive: false,
                              autoDelete: false,
                              arguments: null);

        _channel.QueueDeclare(queue: _settings.ProductionQueueName,
                              durable: true,
                              exclusive: false,
                              autoDelete: false,
                              arguments: null);
    }

    public void StartConsuming()
    {
        EventingBasicConsumer consumer = new EventingBasicConsumer(_channel);
        consumer.Received += (model, ea) =>
        {
            byte[] body = ea.Body.ToArray();
            string message = Encoding.UTF8.GetString(body);
            ProcessMessage(message);

            IBasicProperties replyProperties = _channel.CreateBasicProperties();
            replyProperties.CorrelationId = ea.BasicProperties.CorrelationId;

            _channel.BasicAck(ea.DeliveryTag, false);
        };

        _channel.BasicConsume(queue: _settings.ProductionQueueName,
                             autoAck: false,
                             consumer: consumer);
    }

    private void ProcessMessage(string message)
    {
        CreateProductionRequest? request = JsonSerializer.Deserialize<CreateProductionRequest>(message);

        if (request == null)
            return;

        using (IServiceScope scope = _serviceScopeFactory.CreateScope())
        {
            IMediator mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
            _ = mediator.Send(request, default).Result;
        }
    }

    public void PublishOrder(int orderId, int status)
    {
        OrderMessage message = new(orderId, 6);

        byte[] body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message));

        IBasicProperties properties = _channel.CreateBasicProperties();
        properties.DeliveryMode = 2;

        _channel.BasicPublish(exchange: string.Empty,
                             routingKey: _settings.OrderQueueName,
                             basicProperties: properties,
                             body: body);
    }

    public void Dispose()
    {
        _channel?.Close();
        _connection?.Close();
    }
}