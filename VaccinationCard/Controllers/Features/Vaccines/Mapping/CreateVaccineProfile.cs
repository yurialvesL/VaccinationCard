using AutoMapper;
using VaccinationCard.Application.Commands.Vaccines.CreateVaccine;
using VaccinationCard.Controllers.Features.Vaccines.DTOs.CreateVaccine;

namespace VaccinationCard.Controllers.Features.Vaccines.Mapping;

/// <summary>
/// Create vaccine mapping profile
/// </summary>
public class CreateVaccineProfile : Profile
{
    public CreateVaccineProfile()
    {
        CreateMap<CreateVaccineRequest,CreateVaccineCommand>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));

        CreateMap<CreateVaccineResult, CreateVaccineResponse>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));
    }
}
