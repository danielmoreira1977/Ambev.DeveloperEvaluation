﻿using OpenTelemetry.Context.Propagation;
using System.Diagnostics;

namespace Ambev.DeveloperEvaluation.ServiceBusRabbitMQ;

public class RabbitMQTelemetry
{
    public static string ActivitySourceName = "EventBusRabbitMQ";

    public ActivitySource ActivitySource { get; } = new(ActivitySourceName);
    public TextMapPropagator Propagator { get; } = Propagators.DefaultTextMapPropagator;
}
