namespace VaccinationCard.Controllers.Features.Vaccines.DTOs.DeleteVaccine;

/// <summary>
/// DeleteVaccine Request DTO
/// </summary>
public record DeleteVaccineRequest
{
    /// <summary>
    /// Vaccine unique identifier
    /// </summary>
    public Guid VaccineId { get; set; }
}
