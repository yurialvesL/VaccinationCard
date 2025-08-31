using AutoMapper;
using VaccinationCard.Application.Commands.Vaccines.GetAllVaccine;
using VaccinationCard.Controllers.Features.Vaccines.DTOs.GetAllVacines;

namespace VaccinationCard.Controllers.Features.Vaccines.Mapping;

/// <summary>
/// Configures the mapping profile for retrieving all vaccine-related data.
/// </summary>
public class GetAllVacinesProfile : Profile
{
    public GetAllVacinesProfile()
    {
        CreateMap<GetAllVaccineResult, GetAllVacinesResponse>()
            .ForMember(dest => dest.Vaccines, opt => opt.MapFrom(src => src.Vaccines));
    }
}
