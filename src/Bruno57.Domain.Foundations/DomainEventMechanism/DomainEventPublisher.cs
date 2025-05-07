using Bruno57.Domain.Foundations.Extensions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Bruno57.Domain.Foundations.DomainEventMechanism;
/// <summary>
/// A simple implementation to encapsulate the publishing of the events
/// </summary>
public class DomainEventPublisher : IDomainEventPublisher
{
    private readonly IMediator _mediator;
    private readonly ILogger<DomainEventPublisher> _logger;

    public DomainEventPublisher(IMediator mediator, ILogger<DomainEventPublisher> logger)
    {
        _mediator = mediator.CheckForNull();
        _logger = logger.CheckForNull();
    }

    /// <inheritdoc/>
    public async Task PublishDomainEventsAsync(IEnumerable<DomainNotificationMessageBase> domainEvents, CancellationToken cancellationToken)
    {
        foreach (var domainEvent in domainEvents)
        {
            if (domainEvent is EntityBase entityBaseWithDomainEvent)
            {
                var events = entityBaseWithDomainEvent.DomainEvents.ToList();

                foreach (var eventItem in events)
                {
                    await _mediator.Publish(eventItem, cancellationToken);
                }
            }
            else
            {
                _logger.LogError("The entity of type {Type} does not have domain event mechanism", 
                    domainEvent.GetType().Name);
            }
        }
    }
}
