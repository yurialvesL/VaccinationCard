using MediatR;

namespace VaccinationCard.Application.Commands.Vaccines.CreateVaccine;

/// <summary>
/// CreateVaccine Command object
/// </summary>
public class CreateVaccineCommand : IRequest<CreateVaccineResult>
{
    /// <summary>
    /// Name of vaccine.
    /// </summary>
    public string Name { get; set; } = string.Empty;
}
