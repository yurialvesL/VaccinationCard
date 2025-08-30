namespace VaccinationCard.Domain.Entities;

public class Person : BaseEntitie 
{
    public bool IsAdmin { get; set; }
    public string Name { get; set; } = string.Empty;
    public string CPF { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; }
    public string RefreshToken { get; set; } = string.Empty;
    public DateTime RefreshTokenExpiresAt { get; set; }
    public ICollection<Vaccine> Vaccines { get; set; } = new List<Vaccine>();
}
