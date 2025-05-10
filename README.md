# Bruno57.Domain.Foundations

## Overview
This NuGet package furnishes fundamental building blocks for crafting rich and consistent domain models. It offers inherent support for Value Objects, Entity identification, marking Aggregate Roots, and dispatching domain events.

##  Highlights

✅ EntityBase: A foundational class providing identity management and equality checks for entities.

✅ ValueObject: An abstract class for constructing immutable value objects with intrinsic structural equality.

✅ [AggregateRoot] Attribute: A declarative approach to designate aggregate roots without relying on empty interfaces.

✅ Domain Event Infrastructure: Mechanisms for publishing and processing significant domain occurrences.

##  Installation

Install via NuGet:
```shell
dotnet add package Bruno57.Domain.Foundations
```

##  Foundational Elements
### EntityBase
A base class for your domain entities, equipped with identity comparison and equality operations.
    
### ValueObject
An abstract blueprint for creating immutable value objects where equality is determined by their properties. For example:
```csharp
public class Money : ValueObject
{
    public decimal Amount { get; }
    public string Currency { get; }

    public Money(decimal amount, string currency)
    {
        Amount = amount;
        Currency = currency;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Amount;
        yield return Currency;
    }
}
```

### [AggregateRoot] Attribute
This attribute offers a cleaner alternative to the traditional IAggregateRoot interface for identifying aggregate roots.

```csharp
[AggregateRoot]
public class Order : EntityBase
{
    // Domain logic...
}
```

####  Vacuous Marker Interfaces
In conventional Domain-Driven Design, a common practice involves defining an empty interface like so:
```csharp
public interface IAggregateRoot { }
```
And then having aggregates implement it:
```csharp
public class Order : EntityBase, IAggregateRoot
{
    // Domain logic
}
```
This interface serves solely as a marker, lacking any methods or properties. However, code analysis tools (such as Roslyn analyzers, ReSharper, and StyleCop) often flag this pattern with warnings like:

> IDE0067 / CA1040: "Do not declare empty interfaces."

This is primarily because:

Empty interfaces don't express any behavior, which is a fundamental purpose of interfaces in object-oriented programming.
They can be challenging to inspect programmatically and might introduce ambiguity in larger systems.
Attributes often provide a more suitable mechanism for attaching metadata.

###  The Elegance of [AggregateRoot]
By adopting a custom attribute, you embrace a more semantically precise and analyzer-friendly method for tagging metadata.

```csharp
[AttributeUsage(AttributeTargets.Class, Inherited = false)]
public sealed class AggregateRootAttribute : Attribute
{
}
```

####  Use-case for [AggregateRoot]
If you’re doing something like domain scanning 
(e.g. identifying aggregate roots at startup or validation), 
you can reflect on this attribute easily:

```csharp
var aggregateRootTypes = AppDomain.CurrentDomain
    .GetAssemblies()
    .SelectMany(a => a.GetTypes())
    .Where(t => t.GetCustomAttribute<AggregateRootAttribute>() != null)
    .ToList();
```

This can be useful in:
* Domain model validation routines 
* Automated component registration
* Code generation and source generation techniques
* Documentation generation tools

### Domain Event Dispatching
Built-in support to create, add, and consume domain events from aggregates.

```csharp
public class User : EntityBase
{
    public string Email { get; private set; }

    public User(int id, string email)
        : base(id)
    {
        Email = email;
        AddDomainEvent(new UserCreatedDomainEvent(id, email));
    }
}
```

## ✅ When to Use
Use this package in your domain layer when you want to:

* Move beyond primitive types by employing strongly-typed domain primitives.
* Implement domain objects that inherently validate their state.
* Construct more articulate and maintainable domain models.
* Consistently enforce critical business rules.
* Manage validation outcomes without resorting to exceptions.
* Model equality based on the intrinsic attributes of objects rather than their memory locations.

### This library follows core DDD principles:

* **Encapsulation** - Domain objects protect their invariants
* **Immutability** - Value objects are immutable once created
* **Self-validation** - Objects validate themselves upon creation
* **Expressiveness** - Types reflect the ubiquitous language
* **Separation of concerns** - Different base classes for different domain concepts

### Further Exploration

To delve deeper into the concepts of Domain-Driven Design, consider these resources:
* "Domain-Driven Design: Tackling Complexity in the Heart of Software" by Eric Evans
* "Implementing Domain-Driven Design" by Vaughn Vernon
* Articles on Value Objects and DDD by Martin Fowle
