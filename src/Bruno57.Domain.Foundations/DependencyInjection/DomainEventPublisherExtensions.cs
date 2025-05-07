using Bruno57.Domain.Foundations.DomainEventMechanism;
using Microsoft.Extensions.DependencyInjection;

namespace Bruno57.Domain.Foundations.DependencyInjection;
public static class DomainEventPublisherExtensions
{
    public static IServiceCollection AddDomainEventPublisher(this IServiceCollection services)
    {
        services.AddSingleton<IDomainEventPublisher, DomainEventPublisher>();

        return services;
    }
}
