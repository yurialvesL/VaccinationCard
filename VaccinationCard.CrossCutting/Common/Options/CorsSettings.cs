namespace VaccinationCard.CrossCutting.Common.Options;

public class CorsSettings
{
    public string PolicyName { get; set; } = "DefaultCors";
    public string[] AllowedOrigins { get; set; } = Array.Empty<string>();
    public bool AllowCredentials { get; set; } = false;
}
