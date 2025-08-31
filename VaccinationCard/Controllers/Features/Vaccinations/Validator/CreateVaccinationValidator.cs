using FluentValidation;
using VaccinationCard.Controllers.Features.Vaccinations.DTOs.CreateVaccination;

namespace VaccinationCard.Controllers.Features.Vaccinations.Validator;

public class CreateVaccinationValidator : AbstractValidator<CreateVaccinationRequest>
{
    public CreateVaccinationValidator()
    {
        RuleFor(x => x.PersonId)
            .NotEmpty()
            .WithMessage("The person id dont't be empty.");

        RuleFor(x => x.PersonId)
            .Must(id => id != Guid.Empty)
            .WithMessage("The person id must be a valid, non-empty GUID.");

        RuleFor(x => x.VaccineId)
            .NotEmpty()
            .WithMessage("The vaccine id dont't be empty.");

        RuleFor(x => x.VaccineId)
            .Must(id => id != Guid.Empty)
            .WithMessage("The vaccine id must be a valid, non-empty GUID.");

        RuleFor(x => x.Dose)
            .IsInEnum()
            .WithMessage("The dose must be a valid enum value.");
    }
}
