using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VaccinationCard.Application.Common.DTOs;
using VaccinationCard.CrossCutting.Common.Exceptions;
using VaccinationCard.Domain.Interfaces;

namespace VaccinationCard.Application.Commands.Vaccinations.GetVaccinationByPersonId;

public class GetVaccinationByPersonIdHandler : IRequestHandler<GetVaccinationByPersonIdCommand, GetVaccinationByPersonIdResult>
{
    private readonly IVaccinationRepository _vaccinationRepository;

    private readonly IPersonRepository _personRepository;

    private readonly ILogger<GetVaccinationByPersonIdHandler> _logger;

    private readonly IMapper _mapper;
    public GetVaccinationByPersonIdHandler(IVaccinationRepository vaccinationRepository, IPersonRepository personRepository, ILogger<GetVaccinationByPersonIdHandler> logger, IMapper mapper)
    {
        _vaccinationRepository = vaccinationRepository;
        _personRepository = personRepository;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<GetVaccinationByPersonIdResult> Handle(GetVaccinationByPersonIdCommand request, CancellationToken cancellationToken)
    {
        var person = await _personRepository.GetPersonByIdAsync(request.PersonId, cancellationToken);

        if(person is null)
            throw new NotFoundException($"Person with id:{request.PersonId} not found");

        var vaccinations = await _vaccinationRepository.GetAllVaccinationsByPersonIdAsync(request.PersonId, cancellationToken);

        var result = _mapper.Map<List<VaccinationSummaryDto>>(vaccinations);

        return _mapper.Map<GetVaccinationByPersonIdResult>(result);
    }
}
