using VaccinationCard.Application.Common.DTOs;

namespace VaccinationCard.Controllers.Features.Vaccines.DTOs.GetAllVacines;

/// <summary>
/// Get all vaccines response DTO
/// </summary>
public record GetAllVacinesResponse
{
    /// <summary>
    /// List of all vaccines
    /// </summary>
    public List<VaccineSummaryDto>? Vaccines { get; set; }
}
