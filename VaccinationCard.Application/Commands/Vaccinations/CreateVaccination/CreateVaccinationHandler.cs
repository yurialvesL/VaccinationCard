using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VaccinationCard.Domain.Interfaces;

namespace VaccinationCard.Application.Commands.Vaccinations.CreateVaccination;

public class CreateVaccinationHandler : IRequestHandler<CreateVaccinationCommand,CreateVaccinationResult>
{
    private readonly IVaccinationRepository _vaccinationRepository;
    private readonly ILogger<CreateVaccinationHandler> _logger;
    private readonly IMapper _mapper;

    public CreateVaccinationHandler(IVaccinationRepository vaccinationRepository, ILogger<CreateVaccinationHandler> logger, IMapper mapper )
    {
        _vaccinationRepository = vaccinationRepository;
        _logger = logger;
        _mapper = mapper;   
    }

    public Task<CreateVaccinationResult> Handle(CreateVaccinationCommand request, CancellationToken cancellationToken)
    {
        
    }
}
