using MediatR;

namespace VaccinationCard.Application.Commands.Person.UpdatePerson;

public class UpdatePersonCommand : IRequest<UpdatePersonResult>
{
    public string CPF { get; set; } = string.Empty;
    public bool IsAdmin { get; set; }
}
