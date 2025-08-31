using AutoMapper;
using VaccinationCard.Application.Commands.Person.DeletePerson;
using VaccinationCard.Controllers.Features.Person.DTOs.DeletePersonById;

namespace VaccinationCard.Controllers.Features.Person.Mapping;

/// <summary>
/// DeletePersonProfile mapping profile
/// </summary>
public class DeletePersonProfile : Profile
{
    public DeletePersonProfile()
    {
        CreateMap<DeletePersonByIdRequest, DeletePersonCommand>()
            .ForMember(dest => dest.PersonId, opt => opt.MapFrom(src => src.PersonId));

        CreateMap<DeletePersonResult, DeletePersonByIdResponse>()
            .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => src.IsDelete));
    }
}
