using VaccinationCard.Application.Common.DTOs;

namespace VaccinationCard.Application.Commands.Vaccinations.GetVaccinationByPersonId;

/// <summary>
/// Represents the result of retrieving vaccination records for a specific person by their identifier.
/// </summary>
/// <remarks>This class contains a collection of vaccination summaries associated with the specified
/// person.</remarks>
public class GetVaccinationByPersonIdResult
{
    /// <summary>
    /// Vaccination records associated with the person.
    /// </summary>
    public List<VaccinationSummaryDto>? Vaccinations { get; set; } = null;
}
