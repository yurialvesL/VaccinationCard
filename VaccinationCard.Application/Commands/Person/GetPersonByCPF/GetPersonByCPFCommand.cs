using MediatR;

namespace VaccinationCard.Application.Commands.Person.GetPersonByCPF;

public class GetPersonByCPFCommand : IRequest<GetPersonByCPFResult>
{
    public string CPF { get; set; } = string.Empty;
}
