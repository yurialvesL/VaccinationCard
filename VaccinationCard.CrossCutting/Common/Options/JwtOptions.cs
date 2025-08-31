namespace VaccinationCard.CrossCutting.Common.Options;

/// <summary>
/// JWT options for token generation and validation.
/// </summary>
public class JwtOptions
{
    public string SecretKey { get; set; } = default!;
    public string Issuer { get; set; } = default!;
    public string Audience { get; set; } = default!;
    public int ExpirationInMinutes { get; set; } = 60;
    public int RefreshTokenExpirationInDays { get; set; }
}
