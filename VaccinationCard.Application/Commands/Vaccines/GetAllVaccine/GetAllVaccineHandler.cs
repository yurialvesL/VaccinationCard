using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using VaccinationCard.Application.Common.DTOs;
using VaccinationCard.CrossCutting.Common.Exceptions;
using VaccinationCard.Domain.Interfaces;

namespace VaccinationCard.Application.Commands.Vaccines.GetAllVaccine;

public class GetAllVaccineHandler : IRequestHandler<GetAllVaccineCommand, GetAllVaccineResult>
{
    private readonly IVaccineRepository _vaccineRepository;
    private readonly ILogger<GetAllVaccineHandler> _logger;
    private readonly IMapper _mapper;

    public GetAllVaccineHandler(IVaccineRepository vaccineRepository, ILogger<GetAllVaccineHandler> logger, IMapper mapper)
    {
        _vaccineRepository = vaccineRepository;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<GetAllVaccineResult> Handle(GetAllVaccineCommand request, CancellationToken cancellationToken)
    {
        var vaccinesResult = new List<VaccineSummaryDto>();
        try
        {
            var vaccines = await _vaccineRepository.GetAllVaccinesAsync(cancellationToken);

            if (vaccines is null)
                throw new NotFoundException("No vaccines found in the repository.");

            vaccinesResult = _mapper.Map<List<VaccineSummaryDto>>(vaccines);
        }
        catch (Exception ex)
        {

            _logger.LogError($"An error occurred while retrieving vaccines, message:{ex.Message}");
        }

        return new GetAllVaccineResult { Vaccines = vaccinesResult };
    }
}
