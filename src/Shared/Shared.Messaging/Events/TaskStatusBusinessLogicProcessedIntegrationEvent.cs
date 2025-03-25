namespace Shared.Messaging.Events
{
    public record TaskStatusBusinessLogicProcessedIntegrationEvent(int TaskId) : IntegrationEvent;
}
