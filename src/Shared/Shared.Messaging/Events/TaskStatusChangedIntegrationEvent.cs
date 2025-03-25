namespace Shared.Messaging.Events
{
    public record TaskStatusChangedIntegrationEvent: IntegrationEvent
    {
        public int TaskId { get; set; }
        public string NewStatus { get; set; } = default!;
    }
}
