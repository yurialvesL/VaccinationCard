using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VaccinationCard.Application.Common.DTOs;
using VaccinationCard.CrossCutting.Common.Exceptions;
using VaccinationCard.Domain.Interfaces;

namespace VaccinationCard.Application.Commands.Person.UpdatePerson;

public class UpdatePersonHandler : IRequestHandler<UpdatePersonCommand,UpdatePersonResult>
{
    private readonly IPersonRepository _personRepository;
    private readonly ILogger<UpdatePersonHandler> _logger;
    private readonly IMapper _mapper;

    public UpdatePersonHandler(IPersonRepository personRepository, ILogger<UpdatePersonHandler> logger, IMapper mapper)
    {
        _logger = logger;
        _personRepository = personRepository;
        _mapper = mapper;
    }

    public async Task<UpdatePersonResult> Handle(UpdatePersonCommand request, CancellationToken cancellationToken)
    {
        var person = await _personRepository.GetPersonByCPFAsync(request.CPF, cancellationToken);
        
        if(person == null)
            throw new NotFoundException($"Person with cpf: {request.CPF} not found");

        person.IsAdmin = true;

        await _personRepository.UpdatePersonAsync(person, cancellationToken);

        return _mapper.Map<UpdatePersonResult>(new UpdatePersonResult
        {
            UpdateSuccess = true,
        });
    }
}
