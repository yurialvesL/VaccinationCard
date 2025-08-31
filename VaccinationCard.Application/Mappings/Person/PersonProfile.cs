using AutoMapper;
using VaccinationCard.Application.Commands.Person.CreatePerson;
using VaccinationCard.Application.Commands.Person.GetPersonByCPF;
using VaccinationCard.Application.Commands.Person.UpdatePerson;
using VaccinationCard.Application.Mappings.Converters;
using VaccinationCard.Domain.Entities;
using VaccinationCard.Domain.Enum;

namespace VaccinationCard.Application.Mappings.Person;

/// <summary>
/// Mapping profile for Person entity and related commands/results
/// </summary>
public class PersonProfile : Profile
{
    public PersonProfile()
    {
        CreateMap<CreatePersonCommand, Domain.Entities.Person>()
        .ForMember(d => d.Name, o => o.MapFrom(s => s.Name))
        .ForMember(d => d.CPF, o => o.MapFrom(s => s.CPF))
        .ForMember(d => d.PasswordHash, o => o.MapFrom(s => s.Password))
        .ForMember(d => d.Sex, o => o.ConvertUsing(new StringToSexConverter(), s => s.Sex))
        .ForMember(d => d.DateOfBirth, o => o.MapFrom(s => s.DateOfBirth))
        .ForMember(d => d.IsAdmin, o => o.MapFrom(s => s.IsAdmin));

        CreateMap<Domain.Entities.Person, CreatePersonResult>()
            .ForMember(dest => dest.PersonId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.PersonName, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.CPF, opt => opt.MapFrom(src => src.CPF))
            .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => src.PasswordHash))
            .ForMember(dest => dest.Sex, opt => opt.MapFrom(src => src.Sex))
            .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.DateOfBirth))
            .ForMember(dest => dest.IsAdmin, opt => opt.MapFrom(src => src.IsAdmin));


        CreateMap<Domain.Entities.Person, GetPersonByCPFResult>()
        .ForPath(d => d.Person.Id, o => o.MapFrom(s => s.Id))
        .ForPath(d => d.Person.Name, o => o.MapFrom(s => s.Name))
        .ForPath(d => d.Person.CPF, o => o.MapFrom(s => s.CPF))
        .ForPath(d => d.Person.DateOfBirth, o => o.MapFrom(s => s.DateOfBirth))
        .ForPath(d => d.Person.IsAdmin, o => o.MapFrom(s => s.IsAdmin))
        .ForPath(d => d.Person.Sex, o => o.MapFrom(s => s.Sex));


        CreateMap<UpdatePersonCommand, Domain.Entities.Person>()
            .ForMember(d => d.IsAdmin, opt => opt.MapFrom(src => src.IsAdmin)).ReverseMap();

    }
}
