using VaccinationCard.Domain.Entities;

namespace VaccinationCard.Application.Commands.Vaccines.GetAllVaccine;

/// <summary>
///  GetAllVaccineResult class represents the result of get all record of vaccine or null if not exists.
/// </summary>
public class GetAllVaccineResult
{
    public List<Vaccine>? Vaccines { get; set; }
}
