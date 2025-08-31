using VaccinationCard.Application.Common.DTOs;
using VaccinationCard.Domain.Enum;

namespace VaccinationCard.Application.Commands.Vaccinations.CreateVaccination;

/// <summary>
/// CreateVaccination Object result
/// </summary>
public class CreateVaccinationResult
{
    /// <summary>
    /// Gets or sets the unique identifier for the vaccination record.
    /// </summary>
    public Guid VaccinationId { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier for a person vacinated.
    /// </summary>
    public Guid PersonId { get; set;}

    /// <summary>
    /// Gets or sets the vaccine aplied summary.
    /// </summary>
    public required VaccineSummaryDto Vaccine { get; set; }

    /// <summary>
    /// Gets or sets the dose that has been applied.
    /// </summary>
    public Dose DoseAplicated { get; set; }

    /// <summary>
    /// Gets or sets the DateAplied.
    /// </summary>
    public DateTime DateAplied { get; set; }
}
