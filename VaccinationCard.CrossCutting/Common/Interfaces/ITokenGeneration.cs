namespace VaccinationCard.CrossCutting.Common.Interfaces;

/// <summary>
/// interface for token generation
/// </summary>
public interface ITokenGeneration
{
    /// <summary>
    /// Generates a secure token for the specified user with an expiration date.
    /// </summary>
    /// <param name="userId">The unique identifier of the user for whom the token is being generated.</param>
    /// <param name="secretKey">The secret key used to sign the token. This must be a non-empty string.</param>
    /// <param name="expirationDate">The date and time at which the token will expire.</param>
    /// <returns>A string representing the generated token.</returns>
    string GenerateToken(string userId, string secretKey, DateTime expirationDate);
}
