using AutoMapper;
using VaccinationCard.Application.Commands.Vaccinations.CreateVaccination;
using VaccinationCard.Application.Commands.Vaccinations.GetVaccinationByPersonId;
using VaccinationCard.Application.Common.DTOs;
using VaccinationCard.Domain.Entities;

namespace VaccinationCard.Application.Common.Mappings.Vaccinations;

/// <summary>
/// Automapper profile for creating vaccination records.
/// </summary>
public class VaccinationProfile : Profile
{
    public VaccinationProfile()
    {
        CreateMap<CreateVaccinationCommand, Vaccination>()
           .ForMember(d => d.VaccineId, opt => opt.MapFrom(src => src.VaccineId))
           .ForMember(d => d.Dose, opt => opt.MapFrom(src => src.Dose))
           .ForMember(d => d.PersonId, opt => opt.MapFrom(src => src.PersonId));

        CreateMap<VaccinationSummaryDto,Vaccination>()
            .ForMember(d => d.Id, opt => opt.MapFrom(src => src.VaccinationId))
            .ForMember(d => d.VaccineId, opt => opt.MapFrom(src => src.Vaccine.VaccineId))
            .ForMember(d => d.Dose, opt => opt.MapFrom(src => src.DoseApplied))
            .ForMember(d => d.PersonId, opt => opt.MapFrom(src => src.PersonId))
            .ForMember(d => d.CreatedAt, opt => opt.MapFrom(src => src.DateOfApplied)).ReverseMap();

        CreateMap<CreateVaccinationResult, VaccinationSummaryDto>()
            .ForMember(d => d.VaccinationId, opt => opt.MapFrom(src => src.VaccinationId))
            .ForMember(d => d.PersonId, opt => opt.MapFrom(src => src.PersonId))
            .ForMember(d => d.DoseApplied, opt => opt.MapFrom(src => src.DoseAplicated))
            .ForMember(d => d.DateOfApplied, opt => opt.MapFrom(src => src.DateAplied))
            .ForMember(d => d.Vaccine, opt => opt.MapFrom(src => src.Vaccine)).ReverseMap();

        CreateMap<IEnumerable<VaccinationSummaryDto>, GetVaccinationByPersonIdResult>()
            .ForMember(d => d.Vaccinations, opt => opt.MapFrom(src => src));

    }
}
