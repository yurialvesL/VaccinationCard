namespace VaccinationCard.Controllers.Features.Person.DTOs.UpdatePerson;


/// <summary>
/// Update person Request DTO
/// </summary>
public record UpdatePersonRequest
{
    public string Cpf { get; init; } = string.Empty;
    public bool IsAdmin { get; init; }

}
