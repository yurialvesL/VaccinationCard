using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VaccinationCard.Domain.Interfaces;

namespace VaccinationCard.Application.Commands.Person.GetPersonByCPF;

public class GetPersonByCPFHandler : IRequestHandler<GetPersonByCPFCommand,GetPersonByCPFResult>
{
    private readonly IPersonRepository _personRepository;

    private readonly ILogger<GetPersonByCPFHandler> _logger;

    private readonly IMapper _mapper;

    public GetPersonByCPFHandler(IPersonRepository personRepository, ILogger<GetPersonByCPFHandler> logger, IMapper mapper)
    {
         _personRepository = personRepository;
         _logger = logger;
         _mapper = mapper;
    }

    public async Task<GetPersonByCPFResult> Handle(GetPersonByCPFCommand request, CancellationToken cancellationToken)
    {
        var person = await _personRepository.GetPersonByCPFAsync(request.CPF, cancellationToken);

        if(person == null)
        {
            _logger.LogWarning($"Person with CPF {request.CPF} not found.");
            return new GetPersonByCPFResult() { Person = null };
        }

        return _mapper.Map<GetPersonByCPFResult>(person);
    }
}
