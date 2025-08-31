using VaccinationCard.Domain.Enum;
using VaccinationCard.Domain.Entities;

namespace VaccinationCard.Application.Commands.Person.GetPersonByCPF;

/// <summary>
/// Get Person By CPF Result
/// </summary>
public class GetPersonByCPFResult
{
    public Domain.Entities.Person? Person { get; set; }
}
