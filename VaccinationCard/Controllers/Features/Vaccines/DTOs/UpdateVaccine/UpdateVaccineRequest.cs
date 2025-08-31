namespace VaccinationCard.Controllers.Features.Vaccines.DTOs.UpdateVaccine;

/// <summary>
/// Update Vaccine Request DTO
/// </summary>
public class UpdateVaccineRequest
{
    /// <summary>
    /// Vaccine unique identifier
    /// </summary>
    public Guid VaccineId { get; set; }

    /// <summary>
    /// Vaccine Name
    /// </summary>
    public string Name { get; set; } = string.Empty;
}
