namespace Ambev.DeveloperEvaluation.ServiceBusRabbitMQ;

public class EventBusOptions
{
    public int RetryCount { get; set; } = 10;
    public string SubscriptionClientName { get; set; }
}
