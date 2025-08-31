using AutoMapper;
using VaccinationCard.Application.Commands.Auth;
using VaccinationCard.Controllers.Features.Auth.DTOs;

namespace VaccinationCard.Controllers.Features.Auth.Mapping;

/// <summary>
/// Mapping objects to Auth commands
/// </summary>
public sealed class AuthProfile: Profile
{
    public AuthProfile()
    {
        CreateMap<AuthRequest, AuthPersonCommand>()
        .ForMember(dest => dest.CPF, opt => opt.MapFrom(src => src.CPF))
        .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password));

        CreateMap<AuthPersonResult,AuthResponse>()
            .ForMember(dest => dest.Token, opt => opt.MapFrom(src => src.Token))
            .ForMember(dest => dest.RefreshToken, opt => opt.MapFrom(src => src.RefreshToken))
            .ForMember(dest => dest.RefreshTokenExpiresAt, opt => opt.MapFrom(src => src.RefreshTokenExpiresAt))
            .ForMember(dest => dest.PersonId, opt => opt.MapFrom(src => src.PersonId))
            .ForMember(dest => dest.ExpiresAt, opt => opt.MapFrom(src => src.ExpiresAt));
    }
}
