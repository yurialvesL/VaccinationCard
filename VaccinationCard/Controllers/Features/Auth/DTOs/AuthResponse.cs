namespace VaccinationCard.Controllers.Features.Auth.DTOs;

/// <summary>
/// Represents the response returned after a successful authentication request.
/// </summary>
/// <remarks>This class contains information about the authenticated user, including their unique identifier, 
/// access token, refresh token, and expiration details. The tokens are used to maintain and refresh  the user's
/// authentication session.</remarks>
public class AuthResponse
{
    /// <summary>
    /// Person unique identifier
    /// </summary>
    public string PersonId { get; init; } = string.Empty;
    /// <summary>
    /// Token generated after authentication for accessing protected resources.
    /// </summary>
    public string Token { get; init; } = string.Empty;
    /// <summary>
    /// Refresh token used to obtain a new access token when the current token expires.
    /// </summary>
    public string RefreshToken { get; init; } = string.Empty;
    /// <summary>
    /// Date and time when the access token expires.
    /// </summary>
    public DateTime ExpiresAt { get; init; }
    /// <summary>
    /// Refresh token expiration date and time.
    /// </summary>
    public DateTime RefreshTokenExpiresAt { get; init; }
}
