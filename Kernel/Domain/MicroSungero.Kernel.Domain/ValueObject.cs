using System;
using System.Collections.Generic;
using System.Linq;

namespace MicroSungero.Kernel.Domain
{
  /// <summary>
  /// A small immutable object whose equality is not based on identity,
  /// i.e. two value objects are equal when they have the same value, not necessarily being the same object.
  /// </summary>
  public abstract class ValueObject : IValueObject, ICloneable
  {
    #region Methods

    /// <summary>
    /// Equality operator.
    /// </summary>
    /// <param name="object1">The first value object.</param>
    /// <param name="object2">The second value object.</param>
    /// <returns>True if the first value object is equal to the second value object, else returns False.</returns>
    public static bool operator ==(ValueObject object1, ValueObject object2)
    {
      if (ReferenceEquals(object1, null) ^ ReferenceEquals(object2, null))
      {
        return false;
      }
      return ReferenceEquals(object1, null) || object1.Equals(object2);
    }

    /// <summary>
    /// NonEquality operator.
    /// </summary>
    /// <param name="object1">The first value object.</param>
    /// <param name="object2">The second value object.</param>
    /// <returns>True if the first value object is not equal to the second value object, else returns False.</returns>
    public static bool operator !=(ValueObject object1, ValueObject object2)
    {
      return !(object1 == object2);
    }

    /// <summary>
    /// Get properties of the value object that are identifying it, and also define the equality between value objects.
    /// </summary>
    /// <returns>Indentifying properties of the value object.</returns>
    protected abstract IEnumerable<object> GetEqualityComponents();

    #endregion

    #region object

    public override bool Equals(object obj)
    {
      if (obj == null || obj.GetType() != GetType())
      {
        return false;
      }

      var other = (ValueObject)obj;
      return this.GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
    }

    public override int GetHashCode()
    {
      return GetEqualityComponents()
       .Select(x => x != null ? x.GetHashCode() : 0)
       .Aggregate((x, y) => x ^ y);
    }

    #endregion

    #region ICloneable

    public virtual object Clone()
    {
      return this.MemberwiseClone() as ValueObject;
    }

    #endregion
  }
}
