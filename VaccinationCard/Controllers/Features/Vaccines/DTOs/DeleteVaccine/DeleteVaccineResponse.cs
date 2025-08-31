namespace VaccinationCard.Controllers.Features.Vaccines.DTOs.DeleteVaccine;

/// <summary>
/// Represents the response returned after a vaccine deletion operation.
/// </summary>
public record DeleteVaccineResponse
{
    /// <summary>
    /// if the vaccine was deleted successfully
    /// </summary>
    public bool IsDeleted { get; set; }
}
