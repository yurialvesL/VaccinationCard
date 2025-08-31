using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VaccinationCard.Domain.Interfaces;

namespace VaccinationCard.Application.Commands.Vaccines.DeleteVaccine;

public class DeleteVaccineHandler : IRequestHandler<DeleteVaccineCommand,DeleteVaccineResult>
{
    private readonly IVaccineRepository _vaccineRepository;
    private readonly ILogger<DeleteVaccineHandler> _logger;
    private readonly IMapper _mapper;

    public DeleteVaccineHandler(IVaccineRepository vaccineRepository, ILogger<DeleteVaccineHandler> logger, IMapper mapper)
    {
        _vaccineRepository = vaccineRepository;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<DeleteVaccineResult> Handle(DeleteVaccineCommand request, CancellationToken cancellationToken)
    {
        var exists = await _vaccineRepository.GetVaccineByIdAsync(request.Id, cancellationToken);

        if (exists == null)
        {
            _logger.LogWarning($"Vaccine with ID {request.Id} not found.");

            return new DeleteVaccineResult
            {
                IsDeleted = null
            };
        }

        var isDeleted = await _vaccineRepository.DeleteVaccineAsync(request.Id, cancellationToken);

        if (!isDeleted)
        {
            _logger.LogError($"Failed to delete vaccine with ID {request.Id}.");

            return new DeleteVaccineResult
            {
                IsDeleted = false
            };
        }

        _logger.LogInformation($"Vaccine with ID {request.Id} deleted successfully.");

        return new DeleteVaccineResult
        {
            IsDeleted = true
        };
    }
}
