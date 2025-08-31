namespace VaccinationCard.Controllers.Features.Vaccines.DTOs.CreateVaccine;

/// <summary>
/// Vaccine creation request DTO
/// </summary>
public record CreateVaccineRequest
{
    /// <summary>
    /// Name of Vaccine
    /// </summary>
    public string Name { get; set; } = string.Empty;
}
