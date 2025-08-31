using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VaccinationCard.Application.Commands.Person.UpdatePerson;
using VaccinationCard.Domain.Entities;
using VaccinationCard.Domain.Interfaces;

namespace VaccinationCard.Application.Commands.Vaccines.UpdateVaccine;

public class UpdateVaccineHandler : IRequestHandler<UpdateVaccineCommand,UpdateVaccineResult>
{
    private readonly IVaccineRepository _vaccineRepository;
    private readonly ILogger<UpdateVaccineHandler> _logger;
    private readonly IMapper _mapper;

    public UpdateVaccineHandler(IVaccineRepository vaccineRepository, ILogger<UpdateVaccineHandler> logger, IMapper mapper)
    {
        _vaccineRepository = vaccineRepository;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<UpdateVaccineResult> Handle(UpdateVaccineCommand request, CancellationToken cancellationToken)
    {
        var exists = await _vaccineRepository.GetVaccineByIdAsync(request.Id, cancellationToken);

        if (exists is null)
        {
            _logger.LogWarning($"Vaccine with the id:{request.Id} not found");
            return new UpdateVaccineResult
            {
                UpdateSuccess = false
            };
        }

        var vaccine = _mapper.Map<Vaccine>(request);

        vaccine.UpdateAt = DateTime.UtcNow;

        var updated = await _vaccineRepository.UpdateVaccineAsync(vaccine, cancellationToken);

        if (updated is null)
        {
            _logger.LogWarning($"Vaccine with the id:{request.Id} could not be updated");
            return new UpdateVaccineResult
            {
                UpdateSuccess = false
            };
        }

        return _mapper.Map<UpdateVaccineResult>(new UpdateVaccineResult
        {
            UpdateSuccess = true,
        });
    }
}
