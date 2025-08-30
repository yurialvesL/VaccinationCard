using System.Reflection.Metadata.Ecma335;
using VaccinationCard.Domain.Enum;

namespace VaccinationCard.Domain.Entities;

public class Vaccine : BaseEntitie
{
    public required string Name { get; set; }
    public required List<VaccineType> Dose { get; set; }
    public Person Person { get; set; }
}
