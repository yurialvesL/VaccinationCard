using System.ComponentModel;

namespace VaccinationCard.Domain.Enum;

/// <summary>
/// Enumeration representing the different doses of a vaccine.
/// </summary>
public enum Dose
{
    /// <summary>
    /// First dose of the vaccine.
    /// </summary>
    [Description("1° Dose")]
    firstDose = 1,
    /// <summary>
    /// Second dose of the vaccine.
    /// </summary>
    [Description("2° Dose")]
    secondDose = 2,
    /// <summary>
    /// Third dose of the vaccine.
    /// </summary>
    [Description("3° Dose")]
    thirdDose = 3,
    /// <summary>
    /// First reinforcement dose of the vaccine.
    /// </summary>
    [Description("1° Reforço")]
    firstReinforcement = 4,
    /// <summary>
    /// Second reinforcement dose of the vaccine.
    /// </summary>
    [Description("2° Reforço")]
    secondReinforcement = 5,
}
