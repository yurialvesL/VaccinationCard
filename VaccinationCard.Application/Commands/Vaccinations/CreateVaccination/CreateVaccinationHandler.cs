using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VaccinationCard.Application.Common.DTOs;
using VaccinationCard.CrossCutting.Common.Exceptions;
using VaccinationCard.Domain.Interfaces;

namespace VaccinationCard.Application.Commands.Vaccinations.CreateVaccination;

public class CreateVaccinationHandler : IRequestHandler<CreateVaccinationCommand,CreateVaccinationResult>
{
    private readonly IVaccinationRepository _vaccinationRepository;
    private readonly IPersonRepository _personRepository;
    private readonly IVaccineRepository _vaccineRepository;
    private readonly ILogger<CreateVaccinationHandler> _logger;
    private readonly IMapper _mapper;

    public CreateVaccinationHandler(IVaccinationRepository vaccinationRepository,IPersonRepository personRepository, IVaccineRepository vaccineRepository, ILogger<CreateVaccinationHandler> logger, IMapper mapper )
    {
        _vaccinationRepository = vaccinationRepository;
        _personRepository = personRepository;
        _vaccineRepository = vaccineRepository;
        _logger = logger;
        _mapper = mapper;   
    }

    public async Task<CreateVaccinationResult> Handle(CreateVaccinationCommand request, CancellationToken cancellationToken)
    {
        var personExists = await _personRepository.GetPersonByIdAsync(request.PersonId, cancellationToken);

        if(personExists is null)
            throw new NotFoundException($"Person with id:{request.PersonId} not found");

        var vaccineExists = await _vaccineRepository.GetVaccineByIdAsync(request.VaccineId, cancellationToken);
        
        if(vaccineExists is null)
            throw new NotFoundException($"Vaccine with id:{request.VaccineId} not found");

        var vaccinationExists = await _vaccinationRepository.VaccinationExistsAsync(request.PersonId, request.VaccineId, request.Dose, cancellationToken);

        if (vaccinationExists)
            throw new ConflictException("This dose is already registered for this person and vaccine");

        var hasAllPreviousDoses = await _vaccinationRepository.HasAllPreviousDosesAsync(request.PersonId, request.VaccineId, request.Dose, cancellationToken);
        
        if (hasAllPreviousDoses == false)
            throw new UnprocessableContentException("The person does not have all previous doses for this vaccine");

        var vaccination = _mapper.Map<Domain.Entities.Vaccination>(request);

        try
        {
            var createdVaccination = await _vaccinationRepository.CreateVaccinationAsync(vaccination);

            var result = _mapper.Map<VaccinationSummaryDto>(createdVaccination);

            result.Vaccine = _mapper.Map<VaccineSummaryDto>(vaccineExists);

            return _mapper.Map<CreateVaccinationResult>(result);
        }
        catch (Exception ex)
        {

            throw new Exception("An error occurred while creating the vaccination record.", ex);
        }
    }
}
