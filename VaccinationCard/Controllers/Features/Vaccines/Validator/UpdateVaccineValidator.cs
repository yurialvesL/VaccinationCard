using FluentValidation;
using VaccinationCard.Controllers.Features.Vaccines.DTOs.UpdateVaccine;

namespace VaccinationCard.Controllers.Features.Vaccines.Validator;

/// <summary>
/// Provides validation rules for the <see cref="UpdateVaccineRequest"/> object.
/// </summary>
/// <remarks>This validator ensures that the <see cref="UpdateVaccineRequest"/> contains valid data before
/// processing. Specifically, it validates that the <c>VaccineId</c> is not empty and is a valid GUID, and that the
/// <c>Name</c> is not empty.</remarks>
public class UpdateVaccineValidator : AbstractValidator<UpdateVaccineRequest>
{
    public UpdateVaccineValidator()
    {
        RuleFor(x => x.VaccineId)
            .NotEmpty()
            .WithMessage("The personId dont't be empty.");

        RuleFor(x => x.VaccineId)
            .Must(id => id != Guid.Empty)
            .WithMessage("The ID must be a valid, non-empty GUID.");

        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("The Name don't be empty.");
    }
}
