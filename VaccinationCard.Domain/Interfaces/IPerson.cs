namespace VaccinationCard.Domain.Interfaces;

/// <summary>
/// Interface representing a person with basic properties.
/// </summary>
public interface IPerson
{
    /// <summary>
    /// Id Person
    /// </summary>
    public string Id { get; }

    /// <summary>
    /// Name Person
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Gets or sets a value indicating whether the user has administrative privileges.
    /// </summary>
    public bool IsAdmin { get; }
}
