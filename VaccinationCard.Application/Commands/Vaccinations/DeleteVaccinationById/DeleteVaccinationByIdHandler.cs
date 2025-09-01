using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VaccinationCard.Application.Commands.Vaccines.DeleteVaccine;
using VaccinationCard.Domain.Interfaces;
using VaccinationCard.Infrastructure.Repositories;

namespace VaccinationCard.Application.Commands.Vaccinations.DeleteVaccinationById;

public class DeleteVaccinationByIdHandler : IRequestHandler<DeleteVaccinationByIdCommand, DeleteVaccinationByIdResult>
{
    private readonly IVaccinationRepository _vaccinationRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<DeleteVaccinationByIdHandler> _logger;

    public DeleteVaccinationByIdHandler(IVaccinationRepository vaccinationRepository, IMapper mapper, ILogger<DeleteVaccinationByIdHandler> logger)
    {
        _vaccinationRepository = vaccinationRepository;
        _mapper = mapper;
        _logger = logger;
    }
    public async Task<DeleteVaccinationByIdResult> Handle(DeleteVaccinationByIdCommand request, CancellationToken cancellationToken)
    {
        var exists = await _vaccinationRepository.GetVaccinationByIdAsync(request.VaccinationId, cancellationToken);

        if (exists == null)
        {
            _logger.LogWarning($"Vaccination with ID {request.VaccinationId} not found.");

            return new DeleteVaccinationByIdResult
            {
                IsDeleted = null
            };
        }

        var isDeleted = await _vaccinationRepository.DeleteVaccinationAsync(request.VaccinationId, cancellationToken);

        if (!isDeleted)
        {
            _logger.LogError($"Failed to delete vaccine with ID {request.VaccinationId}.");

            return new DeleteVaccinationByIdResult
            {
                IsDeleted = false
            };
        }

        _logger.LogInformation($"Vaccine with ID {request.VaccinationId} deleted successfully.");

        return new DeleteVaccinationByIdResult
        {
            IsDeleted = true
        };
    }
}
