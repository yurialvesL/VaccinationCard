using AutoMapper;
using VaccinationCard.Application.Commands.Person.CreatePerson;
using VaccinationCard.Controllers.Features.Person.DTOs.CreatePerson;

namespace VaccinationCard.Controllers.Features.Person.Mapping;

/// <summary>
/// Mapping profile for creating a person
/// </summary>
public class CreatePersonProfile : Profile
{
    public CreatePersonProfile()
    {
        CreateMap<CreatePersonRequest, CreatePersonCommand>()
           .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
           .ForMember(dest => dest.CPF, opt => opt.MapFrom(src => src.CPF))
           .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password))
           .ForMember(dest => dest.Sex, opt => opt.MapFrom(src => src.Sex))
           .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.DateOfBirth))
           .ForMember(dest => dest.IsAdmin, opt => opt.MapFrom(src => src.IsAdmin));


        CreateMap<CreatePersonResult, CreatePersonResponse>()
            .ForMember(dest => dest.PersonId, opt => opt.MapFrom(src => src.PersonId))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.PersonName))
            .ForMember(dest => dest.CPF, opt => opt.MapFrom(src => src.CPF))
            .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.DateOfBirth))
            .ForMember(dest => dest.IsAdmin, opt => opt.MapFrom(src => src.IsAdmin))
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt))
            .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt));
    }
}
