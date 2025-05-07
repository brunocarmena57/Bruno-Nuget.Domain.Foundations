using Bruno57.Domain.Foundations.DomainEventMechanism;
using Bruno57.Domain.Foundations;
using Bruno57.Domain.Foundations.DomainEventMechanism;
using Bruno57.Domain.Foundations.Tests.DomainNotificationTestHelpers;
using MediatR;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Bruno57.Domain.Foundations.Tests.DomainEventMechanismTests;
public class DomainEventPublisherTests
{
    private readonly Mock<IMediator> _mockMediator;
    private readonly DomainEventPublisher _sut;
    
    public DomainEventPublisherTests()
    {
        _mockMediator = new Mock<IMediator>();
        var mockLogger = new Mock<ILogger<DomainEventPublisher>>();
        
        _sut = new DomainEventPublisher(_mockMediator.Object, mockLogger.Object);
    }

    [Fact]
    public async Task PublishDomainEvents_ShouldNotCallPublish_WhenDomainEvents_IsEmpty()
    {
        //Arrange/Act
        await _sut.PublishDomainEventsAsync([], CancellationToken.None);

        //Assert
        _mockMediator.Verify(x => x.Publish(It.IsAny<DomainNotificationMessageBase>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task PublishDomainEvents_ShouldNotCallPublish_WhenNotificationMessageTypeIsNotAllowed()
    {
        //Arrange
        var dummyDomainNotificationEvent = new DummyDomainNotificationEvent();
        var messageList = new List<DomainNotificationMessageBase>
        {
            dummyDomainNotificationEvent
        };
        
        //Act
        await _sut.PublishDomainEventsAsync(messageList, CancellationToken.None);

        //Assert
        Assert.Equal("Testing notification message", dummyDomainNotificationEvent.NotificationMessage);
        Assert.IsNotAssignableFrom<EntityBase>(messageList[0]);
        _mockMediator.Verify(x => x.Publish(dummyDomainNotificationEvent, It.IsAny<CancellationToken>()), Times.Never);
    }
    
    [Fact]
    public async Task PublishDomainEvents_ShouldCallPublish_WhenNotificationMessageTypeIsOfCorrectType()
    {
        //Arrange
        var dummyEntity = new DummyEntity();
        dummyEntity.DoSomethingToTriggerAddingEvent();

        var messageList = new List<DomainNotificationMessageBase>
        {
            dummyEntity
        };
        
        //Act
        await _sut.PublishDomainEventsAsync(messageList, CancellationToken.None);

        //Assert
        Assert.Equal("Default message.", dummyEntity.NotificationMessage);
        Assert.IsAssignableFrom<EntityBase>(messageList[0]);

        foreach (var domainEventItem in dummyEntity.DomainEvents)
        {
            _mockMediator.Verify(x => x.Publish(domainEventItem, It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
