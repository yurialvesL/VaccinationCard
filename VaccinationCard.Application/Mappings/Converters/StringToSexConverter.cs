using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VaccinationCard.Domain.Enum;

namespace VaccinationCard.Application.Mappings.Converters;

public sealed class StringToSexConverter : IValueConverter<string?, Sex>
{
    public Sex Convert(string? sourceMember, ResolutionContext context) =>
        Enum.TryParse<Sex>(sourceMember, true, out var sex) ? sex : Sex.Masculine;
}
