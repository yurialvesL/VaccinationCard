using VaccinationCard.Domain.Enum;

namespace VaccinationCard.Controllers.Features.Person.DTOs.GetPersonByCPF;


/// <summary>
/// Get Person by CPF Response DTO
/// </summary>
public record GetPersonByCPFResponse
{
    public Guid PersonId { get; init; }
    public string Name { get; init; } = string.Empty;
    public string CPF { get; init; } = string.Empty;
    public string Sex { get; init; } = string.Empty;
    public bool IsAdmin { get; set; }
    public DateTime DateOfBirth { get; init; }
    public DateTime CreatedAt { get; init; }
    public DateTime UpdateAt { get; init; }
}
