using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Swashbuckle.AspNetCore.Annotations;
using VaccinationCard.Application.Commands.Person.CreatePerson;
using VaccinationCard.Application.Commands.Person.DeletePerson;
using VaccinationCard.Application.Commands.Person.GetPersonByCPF;
using VaccinationCard.Controllers.Features.Person.DTOs.CreatePerson;
using VaccinationCard.Controllers.Features.Person.DTOs.DeletePersonById;
using VaccinationCard.Controllers.Features.Person.DTOs.GetPersonByCPF;
using VaccinationCard.Controllers.Features.Person.DTOs.UpdatePerson;

namespace VaccinationCard.Controllers.Features.Person;


[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
[ApiConventionType(typeof(DefaultApiConventions))]
public class PersonController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public PersonController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }
    

    [HttpPost("CreatePerson")]
    [EnableRateLimiting("fixed-1m")]
    [SwaggerOperation(Summary = "Creates a new person", Description = "Creates a new person in the system")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> CreatePerson([FromBody] CreatePersonRequest request, CancellationToken cancellationToken)
    {
        var validator = new Validator.CreatePersonValidator();

        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var command = _mapper.Map<CreatePersonCommand>(request);

        var result = await _mediator.Send(command, cancellationToken);

        var response = _mapper.Map<CreatePersonResponse>(result);
        
        return Created(string.Empty, response);
    }


    [HttpGet("GetPersonByCPF")]
    [Authorize]
    [SwaggerOperation(Summary = "Gets a person by CPF", Description = "Retrieves a person's details using their CPF number")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> GetPersonByCPF([FromQuery] string cpf, CancellationToken cancellationToken)
    {
        var request = new GetPersonByCPFRequest { CPF = cpf };

        var validator = new Validator.GetPersonByCPFValidator();

        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if(!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var command = _mapper.Map<GetPersonByCPFCommand>(request);

        var result = await _mediator.Send(command, cancellationToken);

        if (result is null)
            return NotFound("Person not found");

        var response = _mapper.Map<GetPersonByCPFResponse>(result);

        return Ok(response);
    }


    [HttpPut("UpdatePerson")]
    [Authorize]
    [SwaggerOperation(Summary = "Update a person to admin", Description ="Update a person to admin")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> UpdatePerson([FromBody] UpdatePersonRequest updatePerson, CancellationToken cancellationToken)
    {
        var validator = new Validator.UpdatePersonValidator();

        var validationResult = await validator.ValidateAsync(updatePerson, cancellationToken);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var command = _mapper.Map<Application.Commands.Person.UpdatePerson.UpdatePersonCommand>(updatePerson);

        var result = await _mediator.Send(command, cancellationToken);

        if(!result.UpdateSuccess)
            return BadRequest("An error occured, try again later");

        return NoContent();
    }


    [HttpDelete("DeletePersonById")]
    [Authorize]
    [SwaggerOperation(Summary = "Deletes a person by ID", Description = "Deletes a person from the system using their unique identifier")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> DeletePersonById([FromQuery] Guid id, CancellationToken cancellationToken)
    {
        var request = new DeletePersonByIdRequest { PersonId = id };

        var validator = new Validator.DeletePersonValidator();

        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var command = _mapper.Map<DeletePersonCommand>(request);

        var result = await _mediator.Send(command, cancellationToken);

        var response = _mapper.Map<DeletePersonByIdResponse>(result);

        if(response.IsDeleted is null)
            return NotFound("Person not found");

        if (response.IsDeleted is false)
            return BadRequest("An error occured, try again later");

        return NoContent();
    }
}
