using MicroSungero.Kernel.Domain.Entities;

namespace MicroSungero.Kernel.Domain
{
  /// <summary>
  /// Service that manages entity lifetime.
  /// </summary>
  public interface IEntityLifetimeService
  {
    /// <summary>
    /// Handle after entity created.
    /// </summary>
    /// <param name="entity">Created entity.</param>
    void OnEntityCreated(IEntity entity);

    /// <summary>
    /// Handle after entity saved.
    /// </summary>
    /// <param name="entity">Saved entity.</param>
    void OnEntitySaved(IEntity entity);

    /// <summary>
    /// Handle after entity deleted.
    /// </summary>
    /// <param name="entity">Deleted entity.</param>
    void OnEntityDeleted(IEntity entity);

    /// <summary>
    /// Handle before save entity.
    /// </summary>
    /// <param name="entity">Saving entity.</param>
    void OnBeforeSaveEntity(IEntity entity);

    /// <summary>
    /// Handle before delete entity.
    /// </summary>
    /// <param name="entity">Deleting entity.</param>
    void OnBeforeDeleteEntity(IEntity entity);
  }
}
