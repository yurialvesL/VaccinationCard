using VaccinationCard.Domain.Enum;

namespace VaccinationCard.Domain.Entities;

public class Vaccine : BaseEntitie
{
    public required string Name { get; set; }
    public required List<Dose> Dose { get; set; }
    public required Person Person { get; set; }
}
