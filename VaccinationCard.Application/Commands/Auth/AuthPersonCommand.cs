using MediatR;

namespace VaccinationCard.Application.Commands.Auth;

/// <summary>
/// Authentication command for a person, containing cpf and password.
/// </summary>
public class AuthPersonCommand : IRequest<AuthPersonResult>
{
    /// <summary>
    /// Cpf of the person to be authenticated.
    /// </summary>
    public string CPF { get; set; } = string.Empty;

    /// <summary>
    /// Password of the person to be authenticated.
    /// </summary>
    public string Password { get; set; } = string.Empty;
}
