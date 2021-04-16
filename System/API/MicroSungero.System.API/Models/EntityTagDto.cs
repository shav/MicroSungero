using AutoMapper;
using MicroSungero.System.Domain;

namespace MicroSungero.System.API.Models
{
  /// <summary>
  /// Entity tag DTO.
  /// </summary>
  [AutoMap(sourceType: typeof(EntityTag))]
  public class EntityTagDto
  {
    /// <summary>
    /// Tag name.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Tag color.
    /// </summary>
    public string Color { get; set; }
  }
}
