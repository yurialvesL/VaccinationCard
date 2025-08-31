namespace VaccinationCard.Controllers.Features.Person.DTOs.CreatePerson;

public class CreatePersonResponse
{
    public Guid PersonId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string CPF { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; }
    public DateTime CreatedAt { get; set; } 
    public DateTime UpdatedAt { get; set; } 
}
