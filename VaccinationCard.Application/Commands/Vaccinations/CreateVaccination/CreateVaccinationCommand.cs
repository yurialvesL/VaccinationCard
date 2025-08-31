using MediatR;
using VaccinationCard.Domain.Enum;

namespace VaccinationCard.Application.Commands.Vaccinations.CreateVaccination;

/// <summary>
/// CreateVaccination Command object
/// </summary>
public class CreateVaccinationCommand : IRequest<CreateVaccinationResult>
{
    /// <summary>
    /// unique identifier of the person to be vaccinated
    /// </summary>
    public Guid PersonId { get; set; }
    /// <summary>
    /// unique identifier of the vaccine to be administered 
    /// </summary>
    public Guid VaccineId { get; set; }
    /// <summary>
    /// Doses that have been administered to this vaccine
    /// </summary>
    public Dose Dose { get; set; }
}
