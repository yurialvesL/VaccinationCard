using VaccinationCard.Domain.Enum;

namespace VaccinationCard.Application.Common.DTOs;

/// <summary>
/// Summary of Vaccination entitie
/// </summary>
public sealed record VaccinationSummaryDto
{

    /// <summary>
    /// Unique identifier of Vaccination
    /// </summary>
    public Guid VaccinationId { get; set; }

    /// <summary>
    /// Unique identifier of PersonId
    /// </summary>
    public Guid  PersonId { get; set; }

    /// <summary>
    /// Summary of Vaccine entity
    /// </summary>
    public required VaccineSummaryDto Vaccine { get; set; }

    /// <summary>
    /// Dose applied in vaccination
    /// </summary>
    public Dose DoseApplied { get; set; }

    /// <summary>
    /// Date of aplied vaccination
    /// </summary>
    public DateTime DateOfApplied { get; set; }

}
