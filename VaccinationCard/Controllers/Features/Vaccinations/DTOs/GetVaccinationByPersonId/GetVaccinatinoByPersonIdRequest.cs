namespace VaccinationCard.Controllers.Features.Vaccinations.DTOs.GetVaccinationByPersonId;

/// <summary>
/// Represents a request to retrieve vaccination information for a specific person.
/// </summary>
/// <remarks>This request is identified by the unique <see cref="PersonId"/> of the person whose vaccination
/// details are being queried.</remarks>
public sealed record GetVaccinatinoByPersonIdRequest
{
    /// <summary>
    /// person unique identifier
    /// </summary>
    public required Guid PersonId { get; set; }
}
