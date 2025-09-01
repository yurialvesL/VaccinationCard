using VaccinationCard.Domain.Enum;
using VaccinationCard.Domain.Entities;
using VaccinationCard.Application.Common.DTOs;

namespace VaccinationCard.Application.Commands.Person.GetPersonByCPF;

/// <summary>
/// Get Person By CPF Result
/// </summary>
public class GetPersonByCPFResult
{
    /// <summary>
    /// Gets or sets the summary information for a person.
    /// </summary>
    public PersonSummaryDto? Person { get; set; }
}
