using VaccinationCard.Application.Common.DTOs;

namespace VaccinationCard.Controllers.Features.Vaccinations.DTOs.GetVaccinationByPersonId;

/// <summary>
/// Represents the response containing a list of vaccinations associated with a specific person.
/// </summary>
/// <remarks>This class is typically used as the result of a query to retrieve vaccination records for a person
/// based on their unique identifier. The <see cref="Vaccinations"/> property provides the details of each vaccination
/// in the form of a collection of <see cref="VaccinationSummaryDto"/> objects.</remarks>
public class GetVaccinationByPersonIdResponse
{
    /// <summary>
    /// List of vaccinations associated with the person.
    /// </summary>
    public List<VaccinationSummaryDto>? Vaccinations { get; set; } = new List<VaccinationSummaryDto>();
}
