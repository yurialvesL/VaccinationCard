using MediatR;

namespace VaccinationCard.Application.Commands.Vaccinations.GetVaccinationByPersonId;

/// <summary>
/// GetVaccinationByPersonId Command object
/// </summary>
public class GetVaccinationByPersonIdCommand : IRequest<GetVaccinationByPersonIdResult>
{
    /// <summary>
    /// Person unique identifier
    /// </summary>
    public Guid PersonId { get; set; }
}
