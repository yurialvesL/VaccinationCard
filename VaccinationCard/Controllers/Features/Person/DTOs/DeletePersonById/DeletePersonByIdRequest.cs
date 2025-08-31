namespace VaccinationCard.Controllers.Features.Person.DTOs.DeletePersonById;

public record DeletePersonByIdRequest
{
    public Guid PersonId { get; init; }
}
