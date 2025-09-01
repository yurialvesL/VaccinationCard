using AutoMapper;
using VaccinationCard.Application.Commands.Vaccinations.DeleteVaccinationById;
using VaccinationCard.Controllers.Features.Vaccinations.DTOs.DeleteVaccinationById;

namespace VaccinationCard.Controllers.Features.Vaccinations.Mapping;

/// <summary>
/// Delete Vaccination By Id Mapping Profile
/// </summary>
public class DeleteVaccinationByIdProfile : Profile
{
    public DeleteVaccinationByIdProfile()
    {
        CreateMap<DeleteVaccinationByIdCommand, DeleteVaccinationByIdRequest>()
           .ForMember(dest => dest.VaccinationId, opt => opt.MapFrom(src => src.VaccinationId)).ReverseMap();

        CreateMap<DeleteVaccinationByIdResult, DeleteVaccinationByIdResponse>()
            .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => src.IsDeleted));
    }
}
