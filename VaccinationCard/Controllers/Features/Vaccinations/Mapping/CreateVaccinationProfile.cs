using AutoMapper;
using VaccinationCard.Application.Commands.Vaccinations.CreateVaccination;
using VaccinationCard.Controllers.Features.Vaccinations.DTOs.CreateVaccination;

namespace VaccinationCard.Controllers.Features.Vaccinations.Mapping;

public class CreateVaccinationProfile: Profile
{
    public CreateVaccinationProfile()
    {
        CreateMap<CreateVaccinationCommand, CreateVaccinationRequest>()
           .ForMember(dest => dest.PersonId, opt => opt.MapFrom(src => src.PersonId))
           .ForMember(dest => dest.VaccineId, opt => opt.MapFrom(src => src.VaccineId))
           .ForMember(dest => dest.Dose, opt => opt.MapFrom(src => src.Dose)).ReverseMap();

        CreateMap<CreateVaccinationResult, CreateVaccinationResponse>()
            .ForMember(dest => dest.VaccinationId, opt => opt.MapFrom(src => src.VaccinationId))
            .ForMember(dest => dest.PersonId, opt => opt.MapFrom(src => src.PersonId))
            .ForMember(dest => dest.Vaccine, opt => opt.MapFrom(src => src.Vaccine))
            .ForMember(dest => dest.DateOfApplication, opt => opt.MapFrom(src => src.DateAplied))
            .ForMember(dest => dest.DoseAplied, opt => opt.MapFrom(src => src.DoseAplicated));
    }
}
