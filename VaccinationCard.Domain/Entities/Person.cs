using VaccinationCard.Domain.Interfaces;

namespace VaccinationCard.Domain.Entities;

/// <summary>
/// Represents an individual person with personal and authentication details.
/// </summary>
public class Person : BaseEntitie, IPerson
{
    /// <summary>
    /// Gets or sets a value indicating whether the user has administrative privileges.
    /// </summary>
    public bool IsAdmin { get; set; }

    /// <summary>
    /// Gets or sets the name associated with the Person.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the CPF (Cadastro de Pessoas Físicas), a unique identifier for individuals in Brazil.
    /// </summary>
    public string CPF { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the hashed representation of the user's password.
    /// </summary>
    public string PasswordHash { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the date of birth.
    /// </summary>
    public DateTime DateOfBirth { get; set; }

    /// <summary>
    /// Gets or sets the refresh token used to obtain a new access token when the current token expires.
    /// </summary>
    public string RefreshToken { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the expiration date and time of the refresh token.
    /// </summary>
    public DateTime RefreshTokenExpiresAt { get; set; }

    /// <summary>
    /// Gets or sets the collection of vaccinations associated with the person.
    /// </summary>
    public ICollection<Vaccination> Vaccinations { get; set; } = new List<Vaccination>();

    /// <summary>
    /// sets the unique identifier for the person as a string.
    /// </summary>
    string IPerson.Id => Id.ToString();

    /// <summary>
    /// sets the name of the person.
    /// </summary>
    string IPerson.Name => Name;

    /// <summary>
    /// sets whether the person has administrative privileges.
    /// </summary>
    bool IPerson.IsAdmin => IsAdmin;
}
