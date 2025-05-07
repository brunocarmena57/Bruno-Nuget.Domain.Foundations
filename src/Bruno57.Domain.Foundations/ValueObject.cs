namespace Bruno57.Domain.Foundations;

/// <summary>
/// Value Object logic base class.
/// <para>An object that represents a descriptive aspect of the domain
/// with no conceptual identity is called a Value Object
/// </para>
/// </summary>
public abstract class ValueObject
{
    /// <summary>
    /// A necessary addition to the Value Object base to allow for including and excluding fields from the comparison.
    /// </summary>
    /// <returns></returns>
    protected abstract IEnumerable<object> GetEqualityComponents();
    
    /// <summary>
    /// To implement equality instead of identity, which is the default.
    /// Why did I override this? By default, DotNet uses Reference Equality when comparing objects.
    /// However, for Value Objects, two instances should be considered equal if all of their properties/attributes are equal,
    /// and not just because they point to the same memory location.
    /// </summary>
    /// <param name="obj"></param>
    /// <returns>Boolean</returns>
    public override bool Equals(object? obj)
    {
        if (obj == null)
            return false;

        if (GetType() != obj.GetType())
            return false;

        var valueObject = (ValueObject)obj;

        return GetEqualityComponents().SequenceEqual(valueObject.GetEqualityComponents());
    }

    /// <summary>
    /// Objects that are considered equal, for Value Objects, they must also have the same hash code.
    /// Hash codes are used in collections.
    /// I've overridden GetHashCode to generate new hash code based on the value of the attributes (obj).
    /// </summary>
    /// <returns>int</returns>
    public override int GetHashCode()
    {
        return GetEqualityComponents()
            .Aggregate(1, (current, obj) =>
            {
                unchecked
                {
                    return current * 23 + obj.GetHashCode();
                }
            });
    }

    /// <summary>
    /// The == operator to make it possible to use it when comparing two objects
    /// </summary>
    /// <param name="one"></param>
    /// <param name="two"></param>
    /// <returns>Boolean</returns>
    public static bool operator ==(ValueObject one, ValueObject two)
    {
        return EqualOperator(one, two);
    }

    /// <summary>
    /// The != operator to make it possible to use it when comparing two objects
    /// </summary>
    /// <param name="one"></param>
    /// <param name="two"></param>
    /// <returns>Boolean</returns>
    public static bool operator !=(ValueObject one, ValueObject two)
    {
        return NotEqualOperator(one, two);
    }
    
    private static bool EqualOperator(ValueObject left, ValueObject right)
    {
        if (ReferenceEquals(left, null) ^ ReferenceEquals(right, null))
        {
            return false;
        }
        return ReferenceEquals(left, right) || left!.Equals(right);
    }

    private static bool NotEqualOperator(ValueObject left, ValueObject right)
    {
        return !EqualOperator(left, right);
    }
}
