using System.ComponentModel.DataAnnotations.Schema;
using MediatR;

namespace Bruno57.Domain.Foundations.DomainEventMechanism;

/// <summary>
/// Base notification message.
/// </summary>
public abstract class DomainNotificationMessageBase : INotification
{
    [NotMapped]
    public string? NotificationMessage { get; protected set; } = "Default message.";
}
