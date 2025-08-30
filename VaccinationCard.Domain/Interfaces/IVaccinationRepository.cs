using VaccinationCard.Domain.Entities;

namespace VaccinationCard.Domain.Interfaces;

/// <summary>
/// Defines a contract for accessing and managing vaccination-related data.
/// </summary>
/// <remarks>This interface provides methods for retrieving, adding, updating, and deleting vaccination records.
/// Implementations of this interface should handle data persistence and retrieval for vaccination-related
/// entities.</remarks>
public interface IVaccinationRepository
{
    /// <summary>
    /// Creates a new vaccination record in the repository.
    /// </summary>
    /// <param name="vaccination"></param>
    /// <returns></returns>
    Task<Vaccination> CreateVaccinationAsync(Vaccination vaccination);

    /// <summary>
    /// Gets all vaccination records from the repository by person id.
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<List<Vaccination>> GetAllVaccinationsByPersonIdAsync(Guid personId,CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets Vaccination by its unique identifier.
    /// </summary>
    /// <param name="vaccinationId"> unique identifier.</param>
    /// <param name="cancellationToken">Cancellation Token</param>
    /// <returns></returns>
    Task<Vaccination?> GetVaccinationByIdAsync(Guid vaccinationId, CancellationToken cancellationToken = default);


    /// <summary>
    /// Delete a vaccination by id.
    /// </summary>
    /// <param name="vaccinationId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<bool> DeleteVaccinationAsync(Guid vaccinationId, CancellationToken cancellationToken = default);

}
