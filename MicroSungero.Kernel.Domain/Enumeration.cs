using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace MicroSungero.Kernel.Domain
{
  /// <summary>
  /// An item of complete ended set of items.
  /// Also used as a container for the set of enumerated items.
  /// </summary>
  public class Enumeration : IComparable
  {
    #region Properties and fields

    /// <summary>
    /// The value of enumeration item.
    /// </summary>
    public string Value { get; private set; }

    #endregion

    #region Methods

    /// <summary>
    /// Get all enumeration items.
    /// </summary>
    /// <typeparam name="T">Type of enumeration.</typeparam>
    /// <returns>Enumeration items.</returns>
    public static IEnumerable<T> GetAll<T>() where T : Enumeration
    {
      var enumFields = typeof(T).GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly)
        .Where(f => typeof(Enumeration).IsAssignableFrom(f.FieldType));
      var enumValues = enumFields.Select(f => f.GetValue(null)).OfType<T>().ToList();

      var enumProperties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly)
        .Where(f => typeof(Enumeration).IsAssignableFrom(f.PropertyType));
      enumValues.AddRange(enumProperties.Select(f => f.GetValue(null)).OfType<T>());

      return enumValues;
    }

    /// <summary>
    /// Create enumeration item from its display value.
    /// </summary>
    /// <typeparam name="T">Type of enumeration.</typeparam>
    /// <param name="value">Enumeration item value.</param>
    /// <returns>Enumeration item.</returns>
    public static T FromValue<T>(string value) where T : Enumeration
    {
      return Parse<T>(value, e => e.Value == value);
    }

    /// <summary>
    /// Parse enumeration item from display value.
    /// </summary>
    /// <typeparam name="T">Type of enumeration.</typeparam>
    /// <param name="displayValue">Display value of enumeration item.</param>
    /// <param name="predicate">Predicate for comparision of enumeration item and its display value.</param>
    /// <returns>Enumeration item.</returns>
    private static T Parse<T>(string displayValue, Func<T, bool> predicate) where T : Enumeration
    {
      var enumValue = GetAll<T>().FirstOrDefault(predicate);
      if (enumValue == null)
        throw new InvalidOperationException($"'{displayValue}' is not a valid {nameof(Value)} of {typeof(T)}");

      return enumValue;
    }

    #endregion

    #region Object

    public override string ToString() => this.Value;

    public override bool Equals(object obj)
    {
      var otherValue = obj as Enumeration;
      if (otherValue == null)
        return false;

      return GetType().Equals(obj.GetType()) &&
        this.Value.Equals(otherValue.Value, StringComparison.OrdinalIgnoreCase);
    }

    public override int GetHashCode() => this.Value.GetHashCode();

    #endregion

    #region IComparable

    public int CompareTo(object other) => this.Value.CompareTo((other as Enumeration)?.Value);

    #endregion

    #region Constructors

    /// <summary>
    /// Create enumeration item.
    /// </summary>
    /// <param name="value">The value of enumeration item.</param>
    public Enumeration(string value)
    {
      if (string.IsNullOrWhiteSpace(value))
        throw new ArgumentNullException(nameof(value), $"{Value} is required for enumeration item");

      this.Value = value;
    }

    #endregion
  }
}
