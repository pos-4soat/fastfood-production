﻿using System.Diagnostics.CodeAnalysis;

namespace fastfood_production.Infra.RabbitMq.Settings;

[ExcludeFromCodeCoverage]
public class RabbitMqSettings
{
    public string HostName { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public string ProductionQueueName { get; set; }
    public string OrderQueueName { get; set; }
}
