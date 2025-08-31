using FluentValidation;
using VaccinationCard.Controllers.Features.Auth.DTOs;
using VaccinationCard.CrossCutting.Common.Extensions;

namespace VaccinationCard.Controllers.Features.Auth.Validator;

/// <summary>
/// Auth request validator using FluentValidation
/// </summary>
public class AuthValidator : AbstractValidator<AuthRequest>
{
    public AuthValidator()
    {

        RuleFor(x => x.CPF)
            .Cpf();

        RuleFor(x => x.Password)
           .NotEmpty().WithMessage("Password is obrigatory")
           .MinimumLength(6).WithMessage("Password must be at least 6 characters long");
    }
}
