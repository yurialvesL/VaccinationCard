using AutoMapper;
using VaccinationCard.Application.Commands.Person.GetPersonByCPF;
using VaccinationCard.Controllers.Features.Person.DTOs.GetPersonByCPF;
using VaccinationCard.CrossCutting.Common.Extensions;

namespace VaccinationCard.Controllers.Features.Person.Mapping;

/// <summary>
/// GetPersonByCPFProfile class for AutoMapper configuration
/// </summary>
public class GetPersonByCPFProfile : Profile
{
    public GetPersonByCPFProfile()
    {
       CreateMap<GetPersonByCPFRequest,GetPersonByCPFCommand>()
            .ForMember(dest => dest.CPF, opt => opt.MapFrom(src => src.CPF));

        CreateMap<GetPersonByCPFResult,GetPersonByCPFResponse>()
            .ForMember(dest => dest.PersonId,opt => opt.MapFrom(src => src.Person.PersonId))
            .ForMember(dest => dest.CPF, opt => opt.MapFrom(src => src.Person.CPF))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Person.Name))
            .ForMember(dest => dest.Sex, opt => opt.MapFrom(src => src.Person.Sex))
            .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.Person.DateOfBirth))
            .ForMember(dest => dest.IsAdmin, opt => opt.MapFrom(src => src.Person.IsAdmin))
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.Person.CreatedAt))
            .ForMember(dest => dest.UpdateAt, opt => opt.MapFrom(src => src.Person.UpdateAt));
    }
}
