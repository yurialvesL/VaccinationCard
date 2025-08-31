using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using VaccinationCard.Application.Commands.Person.CreatePerson;
using VaccinationCard.Controllers.Features.Person.DTOs.CreatePerson;

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
}
