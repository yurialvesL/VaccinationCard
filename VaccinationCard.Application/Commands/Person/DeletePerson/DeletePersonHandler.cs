using MediatR;
using Microsoft.Extensions.Logging;
using VaccinationCard.Domain.Interfaces;

namespace VaccinationCard.Application.Commands.Person.DeletePerson;

public class DeletePersonHandler : IRequestHandler<DeletePersonCommand,DeletePersonResult>
{
    private readonly IPersonRepository _personRepository;
    private readonly ILogger<DeletePersonHandler> _logger;

    public DeletePersonHandler(IPersonRepository personRepository, 
                               ILogger<DeletePersonHandler> logger)
    {
        _personRepository = personRepository;
        _logger = logger;
   
    }

    public async Task<DeletePersonResult> Handle(DeletePersonCommand request, CancellationToken cancellationToken)
    {
        var person = await _personRepository.GetPersonByIdAsync(request.PersonId, cancellationToken);

        if (person == null)
        {
            _logger.LogWarning($"Person with ID {request.PersonId} not found.");

            return new DeletePersonResult
            {
                IsDelete = false
            };
        }

        var success = await _personRepository.DeletePersonAsync(request.PersonId, cancellationToken);

        if (!success)
        {
            _logger.LogError($"Failed to delete person with ID {request.PersonId}.");

            return new DeletePersonResult
            {
                IsDelete = false
            };
        }

        _logger.LogInformation($"Person with ID {request.PersonId} deleted successfully.");

        return new DeletePersonResult
        {
            IsDelete = true
        };
    }
}
