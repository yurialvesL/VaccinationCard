namespace VaccinationCard.Domain.Entities;

/// <summary>
/// Represents the base entity with common properties for all entities in the domain.
/// </summary>
public class BaseEntitie
{
    /// <summary>
    /// Guid unique identifier for the entity.
    /// </summary>
    public Guid Id { get; set; }
    /// <summary>
    /// Creation timestamp of the entity in UTC.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    /// <summary>
    /// Update timestamp of the entity in UTC.
    /// </summary>
    public DateTime UpdateAt { get; set; } = DateTime.Now;
    /// <summary>
    /// Deletion timestamp of the entity in UTC. Null if not deleted.
    /// </summary>
    public DateTime? DeletedAt { get; set; } = null;

    /// <summary>
    /// Constructor that initializes a new instance of the BaseEntity class with a new unique identifier and timestamps.
    /// </summary>
    public BaseEntitie()
    {
        Id = Guid.NewGuid();
        CreatedAt = CreatedAt;
        UpdateAt = UpdateAt;
    }
}
