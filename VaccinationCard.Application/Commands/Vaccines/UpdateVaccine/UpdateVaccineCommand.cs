using MediatR;

namespace VaccinationCard.Application.Commands.Vaccines.UpdateVaccine;


/// <summary>
/// UpdateVaccine Command object
/// </summary>
public class UpdateVaccineCommand : IRequest<UpdateVaccineResult>
{
    /// <summary>
    /// Gets or sets the unique identifier for the entity.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Get or sets the name of the vaccine.
    /// </summary>
    public string Name { get; set; } = string.Empty;
}
