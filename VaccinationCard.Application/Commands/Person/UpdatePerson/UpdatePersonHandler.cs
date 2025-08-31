using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
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
        
        if (person == null)
        {
            _logger.LogWarning($"Person with cpf: {request.CPF} not found");
            return _mapper.Map<UpdatePersonResult>(new UpdatePersonResult
            {
              UpdateSuccess = false,
            });
        }

        var personToUpdate = _mapper.Map(request, person);

        await _personRepository.UpdatePersonAsync(personToUpdate, cancellationToken);

        return _mapper.Map<UpdatePersonResult>(new UpdatePersonResult
        {
            UpdateSuccess = true,
        });
    }
}
