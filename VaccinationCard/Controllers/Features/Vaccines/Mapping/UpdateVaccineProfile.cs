using AutoMapper;
using VaccinationCard.Application.Commands.Vaccines.UpdateVaccine;
using VaccinationCard.Controllers.Features.Vaccines.DTOs.UpdateVaccine;

namespace VaccinationCard.Controllers.Features.Vaccines.Mapping;

/// <summary>
/// UpdateVaccine mapping profile 
/// </summary>
public class UpdateVaccineProfile : Profile
{
    public UpdateVaccineProfile()
    {
        CreateMap<UpdateVaccineRequest, UpdateVaccineCommand>()
           .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.VaccineId))
           .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));

        CreateMap<UpdateVaccineResult, UpdateVaccineResponse>()
            .ForMember(dest => dest.UpdateSuccess, opt => opt.MapFrom(src => src.UpdateSuccess));

    }
}
