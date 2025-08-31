using AutoMapper;
using VaccinationCard.Application.Commands.Vaccines.DeleteVaccine;
using VaccinationCard.Controllers.Features.Person.DTOs.DeletePersonById;
using VaccinationCard.Controllers.Features.Vaccines.DTOs.DeleteVaccine;

namespace VaccinationCard.Controllers.Features.Vaccines.Mapping;

/// <summary>
/// Delete Vaccine mapping profile
/// </summary>
public class DeleteVaccineProfile : Profile
{
    public DeleteVaccineProfile()
    {
        CreateMap<DeleteVaccineRequest, DeleteVaccineCommand>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.VaccineId));

        CreateMap<DeleteVaccineResult, DeletePersonByIdResponse>()
            .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => src.IsDeleted));
    }
}
