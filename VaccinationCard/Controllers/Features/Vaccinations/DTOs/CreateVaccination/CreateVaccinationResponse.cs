using VaccinationCard.Application.Common.DTOs;
using VaccinationCard.Domain.Enum;

namespace VaccinationCard.Controllers.Features.Vaccinations.DTOs.CreateVaccination;

/// <summary>
/// Creteate Vaccination Response DTO
/// </summary>
public record CreateVaccinationResponse
{
    /// <summary>
    /// Vaccination unique identifier   
    /// </summary>
    public Guid VaccinationId { get; set; }

    /// <summary>
    /// Person unique identifier 
    /// </summary>
    public Guid PersonId { get; set; }

    /// <summary>
    /// Vaccine aplied summary
    /// </summary>
    public required VaccineSummaryDto Vaccine { get; set; }

    /// <summary>
    /// Doses aplicated
    /// </summary>
    public Dose DoseAplied { get; set; }

    /// <summary>
    /// Date of application
    /// </summary>
    public DateTime DateOfApplication { get; set; }
}
