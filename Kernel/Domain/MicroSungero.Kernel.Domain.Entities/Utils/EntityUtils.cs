using System;
using System.Linq;

namespace MicroSungero.Kernel.Domain.Entities
{
  /// <summary>
  /// Util methods for entities and entity types.
  /// </summary>
  internal static class EntityUtils
  {
    /// <summary>
    /// Get main entity interface implemented by entity.
    /// </summary>
    /// <param name="entity">Entity.</param>
    /// <returns>Entity interface.</returns>
    public static Type GetEntityInterface(this IEntity entity)
    {
      var entityInterfaces = entity.GetType().GetInterfaces()
        .Where(@interface => typeof(IEntity).IsAssignableFrom(@interface))
        .ToList();

      return entityInterfaces.FirstOrDefault(@interface => 
        !entityInterfaces.Except(new[] { @interface }).Any(t => @interface.IsAssignableFrom(t))
      );
    }
  }
}
