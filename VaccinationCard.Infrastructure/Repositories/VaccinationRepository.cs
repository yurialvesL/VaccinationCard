using Microsoft.EntityFrameworkCore;
using VaccinationCard.Domain.Entities;
using VaccinationCard.Domain.Interfaces;
using VaccinationCard.Infrastructure.Context;

namespace VaccinationCard.Infrastructure.Repositories;

/// <summary>
/// Provides methods for managing vaccination records in the database.
/// </summary>
/// <remarks>This repository offers functionality to create, retrieve, and delete vaccination records. It
/// interacts with the underlying database context to perform these operations.</remarks>
/// <param name="dbContext"></param>
public class VaccinationRepository(ApplicationDbContext dbContext) : IVaccinationRepository
{
    private readonly ApplicationDbContext _dbContext = dbContext;

    /// <summary>
    /// Creates a new vaccination record in the repository.
    /// </summary>
    /// <param name="vaccination"></param>
    /// <returns>Vaccination created</returns>
    public async Task<Vaccination> CreateVaccinationAsync(Vaccination vaccination)
    {
        _dbContext.Vaccinations.Add(vaccination);
        await _dbContext.SaveChangesAsync();
        return vaccination;
    }

    /// <summary>
    /// Delete a vaccination by id.
    /// </summary>
    /// <param name="vaccinationId">vaccination unique identifier</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>True if deleted successuful, false if an error Occurred</returns>
    public async Task<bool> DeleteVaccinationAsync(Guid vaccinationId, CancellationToken cancellationToken = default)
    {
        var deleted = await _dbContext.Vaccinations
           .Where(i => i.Id == vaccinationId)
           .ExecuteDeleteAsync(cancellationToken: cancellationToken);
        return deleted > 1;
    }

    /// <summary>
    ///  Gets all vaccination records from the repository by person id.
    /// </summary>
    /// <param name="personId">Person unique identifier</param>
    /// <param name="cancellationToken">CancelattionToken</param>
    /// <returns>Return a list with all vaccination records</returns>
    public async Task<List<Vaccination>> GetAllVaccinationsByPersonIdAsync(Guid personId, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Vaccinations
            .AsNoTracking()
            .Where(v => v.PersonId == personId)
            .ToListAsync(cancellationToken: cancellationToken);
    }

    /// <summary>
    /// Get Vaccination by its unique identifier.
    /// </summary>
    /// <param name="vaccinationId">Unique identifier</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>Return vaccination if found or null if not found.</returns>
    public async Task<Vaccination?> GetVaccinationByIdAsync(Guid vaccinationId, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Vaccinations
            .AsNoTracking()
            .FirstOrDefaultAsync(v => v.Id == vaccinationId, cancellationToken);
    }
}
