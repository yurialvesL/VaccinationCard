namespace VaccinationCard.Controllers.Features.Person.DTOs.GetPersonByCPF;

/// <summary>
/// Get Person by CPF Request DTO
/// </summary>
public record GetPersonByCPFRequest
{
    public string CPF { get; set; } = string.Empty;
}
