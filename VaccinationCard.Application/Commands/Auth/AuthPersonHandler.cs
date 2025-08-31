using MediatR;
using Microsoft.Extensions.Options;
using System.Security.Cryptography;
using VaccinationCard.CrossCutting.Common.Interfaces;
using VaccinationCard.CrossCutting.Common.Options;
using VaccinationCard.Domain.Interfaces;

namespace VaccinationCard.Application.Commands.Auth;

/// <summary>
/// Handler for authenticating a person and generating JWT tokens
/// </summary>
public class AuthPersonHandler : IRequestHandler<AuthPersonCommand,AuthPersonResult>
{
    private readonly IPersonRepository _personRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IOptions<JwtOptions> _jwtOptions;

    public AuthPersonHandler(IPersonRepository personRepository, 
                            IPasswordHasher passwordHasher,
                            IJwtTokenGenerator jwtTokenGenerator,
                            IOptions<JwtOptions> jwtOptions)
    {
        _personRepository = personRepository;
        _passwordHasher = passwordHasher;
        _jwtTokenGenerator = jwtTokenGenerator;
        _jwtOptions = jwtOptions;   
    }

    public async Task<AuthPersonResult> Handle(AuthPersonCommand request, CancellationToken cancellationToken)
    {
        var person = await _personRepository.GetPersonByCPFAsync(request.CPF, cancellationToken);

        if (person == null || !_passwordHasher.VerifyPassword(person.PasswordHash,request.Password))
            throw new UnauthorizedAccessException("Invalid CPF or password.");

        var nowUtc = DateTime.UtcNow;

        var token = _jwtTokenGenerator.GenerateToken(person);
        
        var accessExpiresAt = nowUtc.AddMinutes(_jwtOptions.Value.ExpirationInMinutes);

        var refreshBytes = RandomNumberGenerator.GetBytes(32);

        var refreshToken = Convert.ToBase64String(refreshBytes)            
                                .Replace('+', '-').Replace('/', '_').TrimEnd('='); 

        var refreshExpiresAt = nowUtc.AddDays(_jwtOptions.Value.RefreshTokenExpirationInDays);

        return new AuthPersonResult
        {
            PersonId = person.Id,
            Token = token,
            ExpiresAt = accessExpiresAt,
            RefreshToken = refreshToken,
            RefreshTokenExpiresAt = refreshExpiresAt
        };
    }
}
