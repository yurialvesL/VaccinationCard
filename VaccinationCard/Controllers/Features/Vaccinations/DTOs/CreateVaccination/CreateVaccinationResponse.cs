using VaccinationCard.Domain.Enum;

namespace VaccinationCard.Controllers.Features.Vaccinations.DTOs.CreateVaccination;

/// <summary>
/// Creteate Vaccination Response DTO
/// </summary>
public record CreateVaccinationResponse
{
    /// <summary>
    /// Person unique identifier 
    /// </summary>
    public Guid PersonId { get; set; }

    /// <summary>
    /// Vaccine unique identifier
    /// </summary>
    public Guid VaccineId { get; set; }

    /// <summary>
    /// Doses aplicated
    /// </summary>
    public Dose Doses { get; set; }

    /// <summary>
    /// Date of application
    /// </summary>
    public DateTime DateOfApplication { get; set; }
}
