using AutoMapper;
using VaccinationCard.Application.Commands.Vaccines.CreateVaccine;
using VaccinationCard.Application.Commands.Vaccines.GetAllVaccine;
using VaccinationCard.Application.Commands.Vaccines.UpdateVaccine;

namespace VaccinationCard.Application.Mappings.Vaccine;

public class VaccineProfile : Profile
{
    public VaccineProfile()
    {
        CreateMap<CreateVaccineCommand, Domain.Entities.Vaccine>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));

        CreateMap<Domain.Entities.Vaccine, CreateVaccineResult>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));

        CreateMap<List<Domain.Entities.Vaccine>,GetAllVaccineResult>()
            .ForMember(dest => dest.Vaccines, opt => opt.MapFrom(src => src));


        CreateMap<UpdateVaccineCommand,Domain.Entities.Vaccine>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));

    }
}
