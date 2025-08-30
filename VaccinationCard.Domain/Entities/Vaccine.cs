namespace VaccinationCard.Domain.Entities;

/// <summary>
/// Represents a vaccine entity with its associated properties and relationships.
/// </summary>
public class Vaccine : BaseEntitie
{
    /// <summary>
    /// Name of the vaccine.
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    ///  Associated vaccinations with this vaccine.
    /// </summary>
    public ICollection<Vaccination> Vaccinations { get; set; } = new List<Vaccination>();
}
