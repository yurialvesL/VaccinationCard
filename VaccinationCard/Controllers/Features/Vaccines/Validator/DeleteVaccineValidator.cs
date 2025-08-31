using FluentValidation;
using VaccinationCard.Controllers.Features.Vaccines.DTOs.DeleteVaccine;

namespace VaccinationCard.Controllers.Features.Vaccines.Validator;

/// <summary>
/// DeleteVaccine request validator
/// </summary>
public class DeleteVaccineValidator : AbstractValidator<DeleteVaccineRequest>
{
    public DeleteVaccineValidator()
    {
        RuleFor(x => x.VaccineId)
            .NotEmpty()
            .WithMessage("The personId dont't be empty.");

        RuleFor(x => x.VaccineId)
            .Must(id => id != Guid.Empty)
            .WithMessage("The ID must be a valid, non-empty GUID.");
    }
}
