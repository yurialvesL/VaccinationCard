using AutoMapper;
using VaccinationCard.Application.Commands.Vaccines.CreateVaccine;
using VaccinationCard.Application.Commands.Vaccines.GetAllVaccine;
using VaccinationCard.Application.Commands.Vaccines.UpdateVaccine;
using VaccinationCard.Application.Common.DTOs;
using VaccinationCard.Domain.Entities;

namespace VaccinationCard.Application.Common.Mappings.Vaccine;

public class VaccineProfile : Profile
{
    public VaccineProfile()
    {
        CreateMap<CreateVaccineCommand, Domain.Entities.Vaccine>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));

        CreateMap<VaccineSummaryDto, Domain.Entities.Vaccine>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.VaccineId))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name)).ReverseMap();

        CreateMap<VaccineSummaryDto, CreateVaccineResult>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.VaccineId));

        CreateMap<List<VaccineSummaryDto>,GetAllVaccineResult>()
            .ForMember(dest => dest.Vaccines, opt => opt.MapFrom(src => src));

        CreateMap<UpdateVaccineCommand,Domain.Entities.Vaccine>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));

    }
}
