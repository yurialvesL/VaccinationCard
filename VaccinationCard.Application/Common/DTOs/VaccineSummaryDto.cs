namespace VaccinationCard.Application.Common.DTOs;

/// <summary>
/// Summary of Vaccine Entitie
/// </summary>
public sealed record VaccineSummaryDto
{
    /// <summary>
    /// Unique identifier of Vaccine
    /// </summary>
    public Guid VaccineId { get; set; }

    /// <summary>
    /// Vaccine Name
    /// </summary>
    public string Name { get; set; } = string.Empty;
}
