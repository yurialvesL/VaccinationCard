using FluentValidation;
using VaccinationCard.Controllers.Features.Person.DTOs.GetPersonByCPF;
using VaccinationCard.CrossCutting.Common.Extensions;

namespace VaccinationCard.Controllers.Features.Person.Validator;


/// <summary>
/// GetPersonByCPFValidator class to validate
/// </summary>
public class GetPersonByCPFValidator : AbstractValidator<GetPersonByCPFRequest>
{
    public GetPersonByCPFValidator()
    {
        RuleFor(x => x.CPF).Cpf();
    }
}
