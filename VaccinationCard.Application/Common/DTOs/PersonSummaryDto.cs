namespace VaccinationCard.Application.Common.DTOs;

/// <summary>
/// Summary of Person entitie
/// </summary>
public sealed record PersonSummaryDto
{   
    /// <summary>
    /// Person unique identifier
    /// </summary>
    public Guid PersonId { get; set; }

    /// <summary>
    /// Name of person
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// CPF unique identifier
    /// </summary>
    public string CPF { get; set; } = string.Empty;

    /// <summary>
    /// Sex of Person
    /// </summary>
    public string Sex { get; set; } = string.Empty;

    /// <summary>
    /// if the user has administrative privileges
    /// </summary>
    public bool IsAdmin { get; set; }

    /// <summary>
    /// Date of Birth of Person
    /// </summary>
    public DateTime DateOfBirth { get; set; }

    /// <summary>
    /// Date of CreatedAt
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Date of Updated
    /// </summary>
    public DateTime UpdateAt { get; set; }

}
