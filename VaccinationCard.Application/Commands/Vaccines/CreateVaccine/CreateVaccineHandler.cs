using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VaccinationCard.Application.Commands.Person.CreatePerson;
using VaccinationCard.CrossCutting.Common.Exceptions;
using VaccinationCard.Domain.Entities;
using VaccinationCard.Domain.Interfaces;

namespace VaccinationCard.Application.Commands.Vaccines.CreateVaccine;

public class CreateVaccineHandler : IRequestHandler<CreateVaccineCommand,CreateVaccineResult>
{
    private readonly IVaccineRepository _vaccineRepository;
    private readonly ILogger<CreateVaccineHandler> _logger;
    private readonly IMapper _mapper;

    public CreateVaccineHandler(IVaccineRepository vaccineRepository, ILogger<CreateVaccineHandler> logger, IMapper mapper)
    {
        _vaccineRepository = vaccineRepository;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<CreateVaccineResult> Handle(CreateVaccineCommand request, CancellationToken cancellationToken)
    {
        var exists = await _vaccineRepository.GetVaccineByNameAsync(request.Name, cancellationToken);

        if(exists is not null)
            throw new ConflictException("Vaccine already registered");

        var vaccine = _mapper.Map<Vaccine>(request);

        var vaccineCreated = await _vaccineRepository.CreateVaccineAsync(vaccine, cancellationToken);

        return _mapper.Map<CreateVaccineResult>(vaccineCreated);
    }
}
