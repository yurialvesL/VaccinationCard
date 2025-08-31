using FluentValidation;
using VaccinationCard.Controllers.Features.Person.DTOs.DeletePersonById;

namespace VaccinationCard.Controllers.Features.Person.Validator;

/// <summary>
/// Validator to delete a person
/// </summary>
public class DeletePersonValidator : AbstractValidator<DeletePersonByIdRequest>
{
    public DeletePersonValidator()
    {
        RuleFor(x => x.PersonId)
            .NotEmpty()
            .WithMessage("The personId dont't be empty.");

        RuleFor(x => x.PersonId)
            .Must(id => id != Guid.Empty)
            .WithMessage("The ID must be a valid, non-empty GUID.");
    }
}
