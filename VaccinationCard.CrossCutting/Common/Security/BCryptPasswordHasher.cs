using VaccinationCard.CrossCutting.Common.Interfaces;

namespace VaccinationCard.CrossCutting.Common.Security;

/// <summary>
/// BCrypt implementation of IPasswordHasher interface
/// </summary>
public class BCryptPasswordHasher : IPasswordHasher
{
    /// <summary>
    /// Hash a password using BCrypt algorithm
    /// </summary>
    /// <param name="password">person password</param>
    /// <returns>Person password hashed</returns>
    public string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }

    /// <summary>
    /// Verify a password against a hashed password using BCrypt algorithm
    /// </summary>
    /// <param name="hashedPassword">password person hashed</param>
    /// <param name="providedPassword">provided person hashed</param>
    /// <returns></returns>
    public bool VerifyPassword(string hashedPassword, string providedPassword)
    {
        return BCrypt.Net.BCrypt.Verify(providedPassword, hashedPassword);
    }
}
