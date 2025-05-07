using System.Diagnostics.CodeAnalysis;

namespace Bruno57.Domain.Foundations.Attributes;

/// <summary>
/// Aggregate Root attribute. Use this to tag/mark your root entities when you want to apply constraints.
/// </summary>
[ExcludeFromCodeCoverage]
[AttributeUsage(AttributeTargets.Class)]
public class AggregateRootAttribute : Attribute
{
    public string? EntityName { get; private set; }

    /// <summary>
    /// Attribute constructor with optional Entity Name parameter.
    /// Use the optional parameter for more granular tagging/marking
    /// </summary>
    /// <param name="entityName"></param>
    public AggregateRootAttribute(string? entityName = null)
    {
        EntityName = entityName;
    }
}
