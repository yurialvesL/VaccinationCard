using System.ComponentModel;

namespace VaccinationCard.Domain.Enum;

public enum VaccineType
{
    [Description("1° Dose")]
    firstDose = 1,
    [Description("2° Dose")]
    secondDose = 2,
    [Description("3° Dose")]
    thirdDose = 3,
    [Description("1° Reforço")]
    firstReinforcement = 4,
    [Description("2° Reforço")]
    secondReinforcement = 5,
}
