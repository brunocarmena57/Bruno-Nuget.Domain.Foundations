using Bruno57.Domain.Foundations.DomainEventMechanism;

namespace Bruno57.Domain.Foundations.Tests.DomainNotificationTestHelpers;

internal class DummyDomainNotificationEvent : DomainNotificationMessageBase
{
    public DummyDomainNotificationEvent()
    {
        NotificationMessage = "Testing notification message";
    }
}
