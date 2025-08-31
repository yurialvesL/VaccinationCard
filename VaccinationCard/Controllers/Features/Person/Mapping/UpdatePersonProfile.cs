using AutoMapper;
using VaccinationCard.Application.Commands.Person.UpdatePerson;
using VaccinationCard.Controllers.Features.Person.DTOs.UpdatePerson;

namespace VaccinationCard.Controllers.Features.Person.Mapping;

/// <summary>
/// UpdatePersonProfile mapping profile
/// </summary>
public class UpdatePersonProfile: Profile
{
    public UpdatePersonProfile()
    {
        CreateMap<UpdatePersonRequest, UpdatePersonCommand>()
            .ForMember(x => x.CPF, opt => opt.MapFrom(src => src.Cpf))
            .ForMember(x => x.IsAdmin, opt => opt.MapFrom(src => src.IsAdmin));

        CreateMap<UpdatePersonResult, UpdatePersonResponse>()
            .ForMember(x => x.UpdateSuccess, opt => opt.MapFrom(src => src.UpdateSuccess));
    }
}
