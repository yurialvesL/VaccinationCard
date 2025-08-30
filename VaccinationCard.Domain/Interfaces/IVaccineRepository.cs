using VaccinationCard.Domain.Entities;

namespace VaccinationCard.Domain.Interfaces;

/// <summary>
/// Repository interface for Vaccine entity operations
/// </summary>
public interface IVaccineRepository
{
    /// <summary>
    /// Creates a new vaccine in the repository
    /// </summary>
    /// <param name="vaccine">The vaccine to create</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created vaccine</returns>
    Task<Vaccine> CreateVaccineAsync(Vaccine vaccine, CancellationToken cancellationToken = default);


    /// <summary>
    /// Retrieves a vaccine by their unique identifier
    /// </summary>
    /// <param name="vaccineId">The unique identifier of the vaccine</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The vaccine if found, null otherwise</returns>
    Task<Vaccine?> GetVaccineByIdAsync(Guid vaccineId, CancellationToken cancellationToken = default);


    /// <summary>
    /// Retrieves a list of vaccines from the repository
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns></returns>
    Task<List<Vaccine>> GetAllVaccinesAsync(CancellationToken cancellationToken = default);


    /// <summary>
    /// Deletes a vaccine from the repository
    /// </summary>
    /// <param name="vaccineId">The unique identifier of the user to delete</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if the user was deleted, false if not found</returns>
    Task<bool> DeleteVaccineAsync(Guid vaccineId, CancellationToken cancellationToken = default);


    /// <summary>
    /// Updates an existing vaccine in the repository
    /// </summary>
    /// <param name="vaccine">The vaccine to update</param>
    /// <param name="cancellationToken">Cancellation token</param>  
    /// <returns>The updated vaccine if successful, null if the vaccine does not exist</returns>
    Task<Vaccine?> UpdateVaccineAsync(Vaccine vaccine, CancellationToken cancellationToken = default);
}
