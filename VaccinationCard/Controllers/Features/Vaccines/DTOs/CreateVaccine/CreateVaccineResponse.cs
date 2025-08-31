namespace VaccinationCard.Controllers.Features.Vaccines.DTOs.CreateVaccine;

/// <summary>
/// Vaccine creation response DTO
/// </summary>
public record CreateVaccineResponse
{
    /// <summary>
    /// Unique identifier of the created vaccine.
    /// </summary>
    public Guid Id { get; init; }

    /// <summary>
    /// Name of the created vaccine.
    /// </summary>
    public string Name { get; set; } = string.Empty;
}
