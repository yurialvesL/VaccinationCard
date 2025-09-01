using FluentValidation;
using VaccinationCard.Controllers.Features.Vaccinations.DTOs.GetVaccinationByPersonId;

namespace VaccinationCard.Controllers.Features.Vaccinations.Validator;

/// <summary>
/// Validator for GetVaccinationByPersonIdRequest
/// </summary>
public class GetVaccinationByPersonIdValidator : AbstractValidator<GetVaccinatinoByPersonIdRequest>
{
    public GetVaccinationByPersonIdValidator()
    {
        RuleFor(x => x.PersonId)
            .NotEmpty()
            .WithMessage("The person id dont't be empty.");

        RuleFor(x => x.PersonId)
            .Must(id => id != Guid.Empty)
            .WithMessage("The person id must be a valid, non-empty GUID.");
    }
}
