using MediatR;

namespace VaccinationCard.Application.Commands.Person.DeletePerson;

/// <summary>
/// Delete Person Command     
/// </summary>
public class DeletePersonCommand : IRequest<DeletePersonResult>
{
    /// <summary>
    /// Gets or sets the unique identifier for a person.
    /// </summary>
    public Guid PersonId { get; set; }
}
