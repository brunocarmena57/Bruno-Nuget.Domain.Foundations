using System.ComponentModel.DataAnnotations.Schema;
using Bruno57.Domain.Foundations.DomainEventMechanism;

namespace Bruno57.Domain.Foundations;

/// <summary>
/// Abstract entity base.
/// </summary>
public abstract class EntityBase : DomainNotificationMessageBase
{
    private readonly List<DomainNotificationMessageBase> _domainEvents = [];

    [NotMapped]
    public IReadOnlyCollection<DomainNotificationMessageBase> DomainEvents => _domainEvents.AsReadOnly();

    public int Id { get; protected set; }

    protected EntityBase()
    { }

    protected EntityBase(int id)
    {
        Id = id;
    }

    protected void AddDomainEvent(DomainNotificationMessageBase notificationMessages)
    {
        _domainEvents.Add(notificationMessages);
    }
}
