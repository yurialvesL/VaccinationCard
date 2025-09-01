using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VaccinationCard.Application.Common.DTOs;
using VaccinationCard.CrossCutting.Common.Exceptions;
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

        if (person == null)
            throw new NotFoundException($"Person with CPF {request.CPF} not found.");    

        var personResult = _mapper.Map<PersonSummaryDto>(person);

        return _mapper.Map<GetPersonByCPFResult>(personResult);
    }
}
