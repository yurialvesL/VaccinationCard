using MediatR;

namespace VaccinationCard.Application.Commands.Vaccinations.DeleteVaccinationById;

/// <summary>
/// DeleteVaccinationById Command object
/// </summary>
public class DeleteVaccinationByIdCommand : IRequest<DeleteVaccinationByIdResult>
{
    /// <summary>
    /// Unique identifier of the vaccination to be deleted
    /// </summary>
    public Guid VaccinationId { get; set; }
}
