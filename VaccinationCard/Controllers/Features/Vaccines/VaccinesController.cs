using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using VaccinationCard.Application.Commands.Vaccines.CreateVaccine;
using VaccinationCard.Application.Commands.Vaccines.DeleteVaccine;
using VaccinationCard.Application.Commands.Vaccines.GetAllVaccine;
using VaccinationCard.Application.Commands.Vaccines.UpdateVaccine;
using VaccinationCard.Controllers.Features.Vaccines.DTOs.CreateVaccine;
using VaccinationCard.Controllers.Features.Vaccines.DTOs.DeleteVaccine;
using VaccinationCard.Controllers.Features.Vaccines.DTOs.UpdateVaccine;
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

        return Created(string.Empty, response);
    }

    [HttpGet("GetAllVaccine")]
    [Authorize]
    [SwaggerOperation(Summary = "Gets all vaccines", Description = "Gets all vaccines in the system")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> GetAllVaccine(CancellationToken cancellationToken)
    {
        var command = new GetAllVaccineCommand();

        var result = await _mediator.Send(command, cancellationToken);

        if (result is null)
            return NotFound("No vaccines found");

        return Ok(result);
    }

    [HttpPut("UpdateVaccine")]
    [Authorize]
    [SwaggerOperation(Summary = "Update Vaccine", Description = "Update a vaccine in the system")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> UpdateVaccine([FromBody] UpdateVaccineRequest request, CancellationToken cancellationToken)
    {
        var validator = new UpdateVaccineValidator();

        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var command = _mapper.Map<UpdateVaccineCommand>(request);

        var result = await _mediator.Send(command, cancellationToken);

        if (result is null)
            return NotFound("Vaccine not found");

        return NoContent();
    }

    [HttpDelete("DeleteVaccine")]
    [Authorize]
    [SwaggerOperation(Summary = "Deletes a vaccine", Description = "Deletes a vaccine from the system by its unique identifier")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> DeleteVaccine([FromQuery] Guid id, CancellationToken cancellationToken)
    {
        var request = new DeleteVaccineRequest { VaccineId = id };

        var validator = new DeleteVaccineValidator();

        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var command = _mapper.Map<DeleteVaccineCommand>(request);

        var result = await _mediator.Send(command, cancellationToken);

        if (result.IsDeleted is null)
            return NotFound("Vaccine not found");

        if(result.IsDeleted is false)
            return BadRequest("An error occurred while deleting the vaccine");  

        return NoContent();
    }
}
