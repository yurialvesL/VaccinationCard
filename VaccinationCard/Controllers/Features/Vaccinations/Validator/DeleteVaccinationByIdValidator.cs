using FluentValidation;
using VaccinationCard.Controllers.Features.Vaccinations.DTOs.DeleteVaccinationById;

namespace VaccinationCard.Controllers.Features.Vaccinations.Validator;

/// <summary>
/// Provides validation rules for the <see cref="DeleteVaccinationByIdRequest"/> object.
/// </summary>
/// <remarks>This validator ensures that the <see cref="DeleteVaccinationByIdRequest.VaccinationId"/> property is
/// not empty and represents a valid, non-empty GUID.</remarks>
public class DeleteVaccinationByIdValidator : AbstractValidator<DeleteVaccinationByIdRequest>
{
    public DeleteVaccinationByIdValidator()
    {
        RuleFor(x => x.VaccinationId)
            .NotEmpty()
            .WithMessage("The vaccination id dont't be empty.");
        RuleFor(x => x.VaccinationId)
            .Must(id => id != Guid.Empty)
            .WithMessage("The vaccination id must be a valid, non-empty GUID.");
    }
}
