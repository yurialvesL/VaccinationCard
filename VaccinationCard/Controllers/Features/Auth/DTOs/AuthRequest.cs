namespace VaccinationCard.Controllers.Features.Auth.DTOs;

/// <summary>
/// Dto for authentication request
/// </summary>
public record AuthRequest
{
    /// <summary>
    /// CPF of the person
    /// </summary>
    public string CPF { get; set; } = string.Empty;

    /// <summary>
    /// Password of the person
    /// </summary>
    public string Password { get; set; } = string.Empty;
}
