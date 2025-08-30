using VaccinationCard.Domain.Interfaces;

namespace VaccinationCard.CrossCutting.Common.Interfaces;

/// <summary>
/// jwt token generator interface with a method to generate a token
/// </summary>
public interface IJwtTokenGenerator
{
    /// <summary>
    /// Generete a JWT token for a given person
    /// </summary>
    /// <param name="person"></param>
    /// <returns></returns>
    string GenerateToken(IPerson person);
}
