using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using VaccinationCard.Application.Commands.Vaccines.CreateVaccine;
using VaccinationCard.Controllers.Features.Vaccines.DTOs.CreateVaccine;
using VaccinationCard.Controllers.Features.Vaccines.Validator;

namespace VaccinationCard.Controllers.Features.Vaccines;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
[ApiConventionType(typeof(DefaultApiConventions))]
public class VaccinesController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public VaccinesController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;   
    }

    [HttpPost("CreateVaccine")]
    [Authorize]
    [SwaggerOperation(Summary = "Creates a new vaccine", Description = "Creates a new vaccine in the system")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> CreateVaccine([FromBody] CreateVaccineRequest request, CancellationToken cancellationToken)
    {
        var validator = new CreateVaccineValidator();

        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var command = _mapper.Map<CreateVaccineCommand>(request);

        var result = await _mediator.Send(command, cancellationToken);

        var response = _mapper.Map<CreateVaccineResponse>(result);

        return Created(string.Empty,response);
    }
}
