namespace VaccinationCard.Controllers.Features.Vaccinations.DTOs.DeleteVaccinationById;

/// <summary>
/// Delete Vaccination By Id Request DTO
/// </summary>
public class DeleteVaccinationByIdRequest
{
    /// <summary>
    /// Gets or sets the unique identifier for the vaccination record.
    /// </summary>
    public Guid VaccinationId { get; set; }
}
