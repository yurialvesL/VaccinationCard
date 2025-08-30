namespace VaccinationCard.CrossCutting.Common.Interfaces;

/// <summary>
/// interface for password hashing and verification
/// </summary>
public interface IPasswordHasher
{
    /// <summary>
    /// hash a plain text password
    /// </summary>
    /// <param name="password">password text</param>
    /// <returns></returns>
    string HashPassword(string password);

    /// <summary>
    /// verify a plain text password against a hashed password
    /// </summary>
    /// <param name="hashedPassword">hashed password</param>
    /// <param name="providedPassword">provided password</param>
    /// <returns></returns>
    bool VerifyPassword(string hashedPassword, string providedPassword);    
}
