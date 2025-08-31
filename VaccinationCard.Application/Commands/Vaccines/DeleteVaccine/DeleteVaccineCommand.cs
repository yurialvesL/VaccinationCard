using MediatR;

namespace VaccinationCard.Application.Commands.Vaccines.DeleteVaccine;

/// <summary>
/// DeleteVaccine command object
/// </summary>
public class DeleteVaccineCommand : IRequest<DeleteVaccineResult>
{
    /// <summary>
    /// Vaccine unique identifier
    /// </summary>
    public Guid Id { get; set; }
}
