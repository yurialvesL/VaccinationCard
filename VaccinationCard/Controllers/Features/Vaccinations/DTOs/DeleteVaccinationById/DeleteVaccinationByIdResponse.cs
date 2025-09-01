namespace VaccinationCard.Controllers.Features.Vaccinations.DTOs.DeleteVaccinationById;

/// <summary>
/// Delete Vaccination By Id Response DTO
/// </summary>
public class DeleteVaccinationByIdResponse
{
    /// <summary>
    /// Indicates whether the vaccination record has been successfully deleted.
    /// </summary>
    public bool IsDeleted { get; set; }
}
