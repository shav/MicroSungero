using System;
using System.Collections.Generic;

namespace MicroSungero.Common.Utils
{
  /// <summary>
  /// Linq extensions for enumerable collections.
  /// </summary>
  public static class LinqExtensions
  {
    /// <summary>
    /// Batch add multiple items to the collection.
    /// </summary>
    /// <typeparam name="T">Collection items type.</typeparam>
    /// <param name="collection">The collection which new items will be added to.</param>
    /// <param name="items">Items which will be added to the collection.</param>
    public static void AddRange<T>(this ICollection<T> collection, IEnumerable<T> items)
    {
      if (items == null)
        throw new ArgumentNullException(nameof(items));

      foreach (T item in items)
        collection.Add(item);
    }

    /// <summary>
    /// Batch add multiple items to the dictionary.
    /// </summary>
    /// <typeparam name="TKey">Dictionary keys type.</typeparam>
    /// <typeparam name="TValue">Dictionary values type.</typeparam>
    /// <param name="dictionary">The dictionary which new items will be added to.</param>
    /// <param name="items">Items which will be added to the dictionary.</param>
    public static void AddRange<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, IEnumerable<KeyValuePair<TKey, TValue>> items)
    {
      if (items == null)
        throw new ArgumentNullException(nameof(items));

      foreach (var item in items)
        dictionary[item.Key] = item.Value;
    }
  }
}
