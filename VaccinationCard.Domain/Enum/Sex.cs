using System.ComponentModel;

namespace VaccinationCard.Domain.Enum;

/// <summary>
/// This enumeration representing the sex of person.
/// </summary>
public enum Sex
{
    /// <summary>
    /// Sex Masculine
    /// </summary>
    [Description("Masculino")]
    Masculine,
    /// <summary>
    /// Sex Feminine
    /// </summary>
    [Description("Feminino")]
    Feminine,
}
