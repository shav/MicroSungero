using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MicroSungero.Kernel.Domain;

namespace MicroSungero.Kernel.Data.EntityFramework
{
  /// <summary>
  /// Value converter between Enumeration and its value stored in database.
  /// </summary>
  /// <typeparam name="TEnumeration">Type of enumeration.</typeparam>
  public class EnumerationValueConverter<TEnumeration>: ValueConverter<TEnumeration, string>
    where TEnumeration: Enumeration
  {
    /// <summary>
    /// Create enumeration value converter.
    /// </summary>
    public EnumerationValueConverter()
      : base(v => v.ToString(), s => Enumeration.Parse<TEnumeration>(s))
    {
    }
  }
}
