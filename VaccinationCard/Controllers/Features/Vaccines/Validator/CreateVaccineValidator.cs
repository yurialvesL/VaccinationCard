using FluentValidation;
using VaccinationCard.Controllers.Features.Vaccines.DTOs.CreateVaccine;

namespace VaccinationCard.Controllers.Features.Vaccines.Validator;

/// <summary>
/// Creates a validator for the CreateVaccineRequest model using FluentValidation
/// </summary>
public class CreateVaccineValidator : AbstractValidator<CreateVaccineRequest>
{
    public CreateVaccineValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required")
            .MaximumLength(100).WithMessage("Name must not exceed 100 characters");
    }
}
