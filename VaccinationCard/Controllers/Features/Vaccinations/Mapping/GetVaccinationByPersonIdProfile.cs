using AutoMapper;
using VaccinationCard.Application.Commands.Vaccinations.GetVaccinationByPersonId;
using VaccinationCard.Application.Commands.Vaccines.GetAllVaccine;
using VaccinationCard.Controllers.Features.Vaccinations.DTOs.GetVaccinationByPersonId;

namespace VaccinationCard.Controllers.Features.Vaccinations.Mapping;

/// <summary>
/// Getting mapping profile for getting vaccination by person id
/// </summary>
public class GetVaccinationByPersonIdProfile : Profile
{
    public GetVaccinationByPersonIdProfile()
    {
        CreateMap<GetVaccinatinoByPersonIdRequest,GetVaccinationByPersonIdCommand>()
            .ForMember(dest => dest.PersonId, opt => opt.MapFrom(src => src.PersonId)).ReverseMap();

        CreateMap<GetVaccinationByPersonIdResult,GetVaccinationByPersonIdResponse>()
            .ForMember(dest => dest.Vaccinations, opt => opt.MapFrom(src => src.Vaccinations));
    }
}
