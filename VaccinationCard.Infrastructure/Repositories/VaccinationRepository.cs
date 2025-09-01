using Microsoft.EntityFrameworkCore;
using VaccinationCard.Domain.Entities;
using VaccinationCard.Domain.Enum;
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
            .Include(v => v.Vaccine)
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

    /// <summary>
    /// Verifies if all previous doses exist for a given person and vaccine.
    /// </summary>
    /// <param name="personId">Unique identifier of person</param>
    /// <param name="vaccineId">Unique identifier of vaccine</param>
    /// <param name="dose">Dose to check</param>
    /// <param name="cancellationToken"></param>
    /// <returns>Return true if  Has all previous doses, else false</returns>
    public async Task<bool> HasAllPreviousDosesAsync(Guid personId, Guid vaccineId, Dose dose, CancellationToken cancellationToken = default)
    {
        var desiredDoseNumber = (int)dose;
        if (desiredDoseNumber <= 1)
            return false;

        var prevDose = (Dose)(desiredDoseNumber - 1);

        var previousCount = await _dbContext.Vaccinations
         .AsNoTracking()
         .Where(v => v.PersonId == personId
                  && v.VaccineId == vaccineId
                  && (int)v.Dose < desiredDoseNumber)          
         .Select(v => v.Dose)
         .Distinct()                              
         .CountAsync(cancellationToken);

        return previousCount == (desiredDoseNumber - 1);
    }

    /// <summary>
    /// Gets a value indicating whether a vaccination record exists for the specified person, vaccine, and dose.
    /// </summary>
    /// <param name="personId"></param>
    /// <param name="vaccineId"></param>
    /// <param name="dose"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>If exists return true, else false</returns>
    public async Task<bool> VaccinationExistsAsync(Guid personId, Guid vaccineId, Dose dose, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Vaccinations
            .AsNoTracking()
            .AnyAsync(v => v.PersonId == personId && 
                            v.VaccineId == vaccineId && 
                            v.Dose == dose, cancellationToken);
    }
}
