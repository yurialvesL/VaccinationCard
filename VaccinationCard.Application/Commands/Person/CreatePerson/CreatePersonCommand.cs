using MediatR;
using VaccinationCard.Domain.Enum;

namespace VaccinationCard.Application.Commands.Person.CreatePerson;

/// <summary>
/// Person creation command
/// </summary>
public class CreatePersonCommand : IRequest<CreatePersonResult>
{
    /// <summary>
    /// Gets or sets the CPF (Cadastro de Pessoas Físicas), a unique identifier for individuals in Brazil.
    /// </summary>
    public string CPF { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the name associated with the Person.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the sex of the person.
    /// </summary>
    public string Sex { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the password for the person.
    /// </summary>
    public string Password { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the date of birth.
    /// </summary>
    public DateTime DateOfBirth { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the user has administrative privileges.
    /// </summary>
    public bool IsAdmin { get; set; }
}
