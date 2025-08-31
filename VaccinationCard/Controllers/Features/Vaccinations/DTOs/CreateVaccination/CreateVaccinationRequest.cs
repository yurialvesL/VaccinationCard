using VaccinationCard.Domain.Enum;

namespace VaccinationCard.Controllers.Features.Vaccinations.DTOs.CreateVaccination;

/// <summary>
/// Create Vaccination Request DTO
/// </summary>
public record CreateVaccinationRequest
{
    /// <summary>
    /// unique identifier of the person to be vaccinated
    /// </summary>
    public Guid PersonId { get; set; }
    /// <summary>
    /// unique identifier of the vaccine to be administered 
    /// </summary>
    public Guid VaccineId { get; set; }
    /// <summary>
    /// Doses that have been administered to this vaccine
    /// </summary>
    public Dose Dose { get; set; }
}
