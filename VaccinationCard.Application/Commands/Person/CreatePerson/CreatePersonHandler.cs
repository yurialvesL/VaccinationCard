using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VaccinationCard.Application.Common.DTOs;
using VaccinationCard.CrossCutting.Common.Exceptions;
using VaccinationCard.CrossCutting.Common.Interfaces;
using VaccinationCard.Domain.Interfaces;

namespace VaccinationCard.Application.Commands.Person.CreatePerson;

public class CreatePersonHandler : IRequestHandler<CreatePersonCommand, CreatePersonResult>
{
    private readonly IPersonRepository _personRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly ILogger<CreatePersonHandler> _logger;
    private readonly IMapper _mapper;

    public CreatePersonHandler(IPersonRepository personRepository, 
                               IPasswordHasher passwordHasher, 
                               ILogger<CreatePersonHandler> logger,
                               IMapper mapper)
    {
        _logger = logger;
        _personRepository = personRepository;
        _passwordHasher = passwordHasher;
        _mapper = mapper;
    }

    public async Task<CreatePersonResult> Handle(CreatePersonCommand request, CancellationToken cancellationToken)
    {
        var exists = await _personRepository.GetPersonByCPFAsync(request.CPF, cancellationToken);

        if (exists is not null)
            throw new ConflictException("CPF already registered");

        request.Password = _passwordHasher.HashPassword(request.Password);

        var personEntity = _mapper.Map<Domain.Entities.Person>(request);

        try
        {
          
            await _personRepository.CreatePersonAsync(personEntity, cancellationToken);

        }
        catch (Exception ex)
        {

            _logger.LogError($"An error occured in {nameof(CreatePersonHandler)}, Exception: {ex.Message}");
        }

        var personResult = _mapper.Map<PersonSummaryDto>(personEntity);


        return _mapper.Map<CreatePersonResult>(personResult);
    }
}
