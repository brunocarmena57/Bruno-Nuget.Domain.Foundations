namespace Bruno57.Domain.Foundations.Tests.DomainNotificationTestHelpers;
internal class DummyEntity : EntityBase
{
    internal void DoSomethingToTriggerAddingEvent()
    {
        Id = Random.Shared.Next();
        
        var dummyDomainNotificationEvent = new DummyDomainNotificationEvent();
        AddDomainEvent(dummyDomainNotificationEvent);
    }
}
