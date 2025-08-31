using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace VaccinationCard.CrossCutting.Common.Extensions;


/// <summary>
/// Enumeration extensions for retrieving descriptions and display names.
/// </summary>
public static class EnumExtensions
{
    /// <summary>
    /// Retrieves the description or display name of an enumeration value.
    /// </summary>
    /// <param name="value"></param>
    /// <returns>Retrieve description of enum.</returns>
    public static string GetDescription(this Enum value)
    {
        var type = value.GetType();
        var name = Enum.GetName(type, value);
        if (name is null) return value.ToString();

        var field = type.GetField(name)!;
        return field.GetCustomAttribute<DescriptionAttribute>()?.Description
            ?? field.GetCustomAttribute<DisplayAttribute>()?.GetName()
            ?? name;
    }
}
