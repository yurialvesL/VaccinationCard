using MediatR;
using Microsoft.Extensions.Logging;
using VaccinationCard.CrossCutting.Common.Exceptions;
using VaccinationCard.CrossCutting.Common.Interfaces;
using VaccinationCard.Domain.Entities;
using VaccinationCard.Domain.Interfaces;

namespace VaccinationCard.Application.Commands.Person.CreatePerson;

public class CreatePersonHandler : IRequestHandler<CreatePersonCommand, CreatePersonResult>
{
    private readonly IPersonRepository _personRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly ILogger<CreatePersonHandler> _logger;

    public CreatePersonHandler(IPersonRepository personRepository, IPasswordHasher passwordHasher, ILogger<CreatePersonHandler> logger)
    {
        _logger = logger;
        _personRepository = personRepository;
        _passwordHasher = passwordHasher;
    }

    public async Task<CreatePersonResult> Handle(CreatePersonCommand request, CancellationToken cancellationToken)
    {
        var exists = await _personRepository.GetPersonByCPFAsync(request.CPF, cancellationToken);

        if (exists is not null)
            throw new ConflictException("CPF already registered");


        var personEntity = new VaccinationCard.Domain.Entities.Person
        {
            Name = request.Name,
            CPF = request.CPF,
            DateOfBirth = request.DateOfBirth,
            PasswordHash = _passwordHasher.HashPassword(request.Password),
            IsAdmin = request.IsAdmin
        };

        try
        {
          
            await _personRepository.CreatePersonAsync(personEntity, cancellationToken);

        }
        catch (Exception ex)
        {

            _logger.LogError($"An error occured in {nameof(CreatePersonHandler)}, Exception: {ex.Message}");
        }

        return new CreatePersonResult
        {
            PersonId = personEntity.Id,
            PersonName = personEntity.Name,
            CPF = personEntity.CPF,
            DateOfBirth = personEntity.DateOfBirth
        };
    }
}
