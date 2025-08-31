using FluentValidation;
using VaccinationCard.Controllers.Features.Person.DTOs.CreatePerson;
using VaccinationCard.CrossCutting.Common.Validation;

namespace VaccinationCard.Controllers.Features.Person.Validator;

/// <summary>
/// Validator for creating a new person
/// </summary>
public class CreatePersonValidator : AbstractValidator<CreatePersonRequest>
{
    public CreatePersonValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(100).WithMessage("Name must not exceed 50 characters.");
        RuleFor(x => x.CPF)
            .Cpf();
        RuleFor(x => x.DateOfBirth)
            .NotEmpty().WithMessage("Date of birth is required.")
            .LessThan(DateTime.Now).WithMessage("Date of birth must be in the past.");
        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(8).WithMessage("Password must be at least 8 characters long.")
            .MaximumLength(100).WithMessage("Password must not exceed 100 characters.");
    }
}
