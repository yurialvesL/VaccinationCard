using FluentValidation;
using VaccinationCard.Controllers.Features.Person.DTOs.UpdatePerson;
using VaccinationCard.CrossCutting.Common.Extensions;

namespace VaccinationCard.Controllers.Features.Person.Validator;

/// <summary>
/// Validator for updating a person's information.
/// </summary>
public class UpdatePersonValidator : AbstractValidator<UpdatePersonRequest>
{
    public UpdatePersonValidator()
    {
        RuleFor(x => x.Cpf)
            .Cpf();

        RuleFor(x => x.IsAdmin)
           .NotNull()
           .WithMessage("IsAdmin property don't be null");
    }
}
