using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using VaccinationCard.Application.Commands.Vaccinations.CreateVaccination;
using VaccinationCard.Application.Commands.Vaccinations.DeleteVaccinationById;
using VaccinationCard.Application.Commands.Vaccinations.GetVaccinationByPersonId;
using VaccinationCard.Controllers.Features.Vaccinations.DTOs.CreateVaccination;
using VaccinationCard.Controllers.Features.Vaccinations.DTOs.GetVaccinationByPersonId;

namespace VaccinationCard.Controllers.Features.Vaccinations;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
[ApiConventionType(typeof(DefaultApiConventions))]
public class VaccinationController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public VaccinationController(IMediator mediator, IMapper mapper)
    {
        _mapper = mapper;
        _mediator = mediator;
    }


    [HttpPost("CreateVaccination")]
    [Authorize]
    [SwaggerOperation(Summary = "Creates a new vaccination", Description = "Creates a new vaccination in the system")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> CreateVaccination([FromBody] CreateVaccinationRequest request, CancellationToken cancellationToken)
    {
        var validator = new Validator.CreateVaccinationValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var command = _mapper.Map<CreateVaccinationCommand>(request);

        var result = await _mediator.Send(command, cancellationToken);

        var response = _mapper.Map<CreateVaccinationResponse>(result);

        return Created(string.Empty, response);
    }


    [HttpGet("GetAllVaccinationsByPersonId")]
    [Authorize]
    [SwaggerOperation(Summary = "Gets all vaccinations by person ID", Description = "Gets all vaccinations for a specific person using their ID")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> CreateVaccination([FromQuery] Guid id, CancellationToken cancellationToken)
    {
        var request = new GetVaccinatinoByPersonIdRequest { PersonId = id };

        var validator = new Validator.GetVaccinationByPersonIdValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var command = _mapper.Map<GetVaccinationByPersonIdCommand>(request);

        var result = await _mediator.Send(command, cancellationToken);

        if (result.Vaccinations is null || result.Vaccinations.Count < 1)
            return NotFound("No vaccinations found for this person");

        var response = _mapper.Map<GetVaccinationByPersonIdResponse>(result);

        return Ok(response);
    }


    [HttpDelete("DeleteVaccination")]
    [Authorize]
    [SwaggerOperation(Summary = "Deletes a vaccination", Description = "Deletes a vaccination from the system by its unique identifier")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> DeleteVaccinationById([FromQuery] Guid id, CancellationToken cancellationToken)
    {
        var request = new DTOs.DeleteVaccinationById.DeleteVaccinationByIdRequest { VaccinationId = id };

        var validator = new Validator.DeleteVaccinationByIdValidator();

        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var command = _mapper.Map<DeleteVaccinationByIdCommand>(request);

        var result = await _mediator.Send(command, cancellationToken);

        if (result.IsDeleted is null)
            return NotFound("Vaccination not found");

        return NoContent();
    }
}
