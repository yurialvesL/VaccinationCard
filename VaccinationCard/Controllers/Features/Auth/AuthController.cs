using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Swashbuckle.AspNetCore.Annotations;
using VaccinationCard.Application.Commands.Auth;
using VaccinationCard.Controllers.Features.Auth.DTOs;
using VaccinationCard.Controllers.Features.Auth.Validator;

namespace VaccinationCard.Controllers.Features.Auth;


[ApiController]
[EnableRateLimiting("fixed-1m")]
[Route("api/[controller]")]
[Produces("application/json")]
[ApiConventionType(typeof(DefaultApiConventions))]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public AuthController(IMediator mediator, IMapper mapper)
    {
        _mapper = mapper;
        _mediator = mediator;   
    }

    [HttpPost("Login")]
    [SwaggerOperation(Summary = "Authenticate a user and generate a JWT token.", OperationId ="Login", Description ="Login Person")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> Login([FromBody] AuthRequest request, CancellationToken cancellationToken)
    {
        var validator = new AuthValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var command = _mapper.Map<AuthPersonCommand>(request);
        var response = await _mediator.Send(command, cancellationToken);

        return response is not null
            ? Ok(response)
            : NotFound("User not found or invalid credentials.");
    }

}
