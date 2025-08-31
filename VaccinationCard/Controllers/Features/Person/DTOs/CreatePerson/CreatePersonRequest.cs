using VaccinationCard.Domain.Enum;

namespace VaccinationCard.Controllers.Features.Person.DTOs.CreatePerson;

public record CreatePersonRequest
{
    public string Name { get; init; } = string.Empty;
    public string CPF { get; init; } = string.Empty;
    public string Password { get; init; } = string.Empty;
    public string Sex { get; init; } = string.Empty;
    public DateTime DateOfBirth { get; init; }
    public bool IsAdmin { get; set; }
}
