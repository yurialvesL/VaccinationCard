using FluentValidation;
using System.Text.RegularExpressions;

namespace VaccinationCard.CrossCutting.Common.Validation;

/// <summary>
/// Provides extension methods for validating CPF (Cadastro de Pessoas Físicas) numbers using FluentValidation rules.
/// </summary>
/// <remarks>This class includes an extension method to validate CPF numbers, ensuring they are not empty and
/// conform to the CPF format and checksum rules. The validation logic includes checks for invalid sequences and proper
/// calculation of verification digits.</remarks>
public static class CpfValidationExtensions
{
    public static IRuleBuilderOptions<T, string> Cpf<T>(this IRuleBuilderInitial<T, string> rule)
       => rule
           .NotEmpty().WithMessage("CPF is obrigatory")
           .Must(value => string.IsNullOrWhiteSpace(value) || IsValidCpf(value))
           .WithMessage("invalid CPF");

    private static bool IsValidCpf(string? input)
    {
        if (string.IsNullOrWhiteSpace(input)) return false;

        // Keep only the digits
        var digits = Regex.Replace(input, "[^0-9]", "");
        if (digits.Length != 11) return false;

        // Reject sequences how: 00000000000, 11111111111, etc.
        if (new string(digits[0], 11) == digits) return false;

        // Calcule DV1
        int sum = 0;
        for (int i = 0, weight = 10; i < 9; i++, weight--)
            sum += (digits[i] - '0') * weight;

        int remainder = sum % 11;
        int dv1 = remainder < 2 ? 0 : 11 - remainder;
        if (dv1 != (digits[9] - '0')) return false;

        // Calcule DV2
        sum = 0;
        for (int i = 0, weight = 11; i < 10; i++, weight--)
            sum += (digits[i] - '0') * weight;

        remainder = sum % 11;
        int dv2 = remainder < 2 ? 0 : 11 - remainder;
        if (dv2 != (digits[10] - '0')) return false;

        return true;
    }
}
