using VaccinationCard.Domain.Enum;

namespace VaccinationCard.Application.Commands.Person.CreatePerson;

/// <summary>
/// Create Person Command Result
/// </summary>
public class CreatePersonResult
{
    /// <summary>
    /// Person unique identifier ID
    /// </summary>
    public Guid PersonId { get; set; }

    /// <summary>
    /// Person Name
    /// </summary>
    public string PersonName { get; set; } = string.Empty;  

    /// <summary>
    /// Person CPF
    /// </summary>
    public string CPF { get; set; } = string.Empty;

    /// <summary>
    /// Password person Hash
    /// </summary>
    public string PasswordHash { get; set; } = string.Empty;

    /// <summary>
    /// Date of Birth
    /// </summary>
    public DateTime DateOfBirth { get; set; }

    /// <summary>
    /// Person is admin or not
    /// </summary>
    public bool IsAdmin { get; set; }

    /// <summary>
    /// Sex of the person
    /// </summary>
    public Sex Sex { get; set; }

    /// <summary>
    /// Create date record
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// update date record
    /// </summary>
    public DateTime UpdatedAt { get; set; }
}

