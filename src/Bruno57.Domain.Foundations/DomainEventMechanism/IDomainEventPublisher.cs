namespace Bruno57.Domain.Foundations.DomainEventMechanism;

/// <summary>
/// An interface for publishing events
/// </summary>
public interface IDomainEventPublisher
{
    /// <summary>
    /// Publish events
    /// </summary>
    /// <param name="domainEvents">List of events</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task PublishDomainEventsAsync(IEnumerable<DomainNotificationMessageBase> domainEvents, CancellationToken cancellationToken);
}
