namespace VaccinationCard.Application.Commands.Vaccines.CreateVaccine;

/// <summary>
/// CreateVaccineResult class represents the result of creating a vaccine, containing its unique identifier and name.
/// </summary>
public class CreateVaccineResult
{
    /// <summary>
    /// Vaccine created unique identifier
    /// </summary>
    public Guid Id { get; set; }
    /// <summary>
    /// Name of Vaccine.
    /// </summary>
    public string Name { get; set; } = string.Empty;
}
