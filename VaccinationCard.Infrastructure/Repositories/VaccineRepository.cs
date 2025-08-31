using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using VaccinationCard.Domain.Entities;
using VaccinationCard.Domain.Interfaces;
using VaccinationCard.Infrastructure.Context;

namespace VaccinationCard.Infrastructure.Repositories;

/// <summary>
/// Implementation of IPersonRepository using Entity Framework Core
/// </summary>
/// <remarks>This repository serves as an abstraction layer for interacting with the underlying database context.
/// It is designed to encapsulate data access logic for vaccine entities, ensuring separation of concerns and promoting
/// testability.</remarks>
/// <param name="dbContext"></param>
public class VaccineRepository(ApplicationDbContext dbContext): IVaccineRepository
{
    private readonly ApplicationDbContext _dbContext = dbContext;

    /// <summary>
    /// Creates a new vaccine in the repository
    ///</summary> 
    ///<param name="vaccine">The vaccine to create</param>
    ///<param name="cancellationToken">Cancellation token</param>
    ///<returns>The created vaccine</returns>
    public async Task<Vaccine> CreateVaccineAsync(Vaccine vaccine, CancellationToken cancellationToken = default)
    {
        _dbContext.Vaccines.Add(vaccine);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return vaccine;
    }

    /// <summary>
    /// Deletes a vaccine from the repository by its unique identifier
    /// </summary>
    /// <param name="vaccineId">Vaccine unique identifier</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>True if the exclusion ok, false if an error occurred</returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<bool> DeleteVaccineAsync(Guid vaccineId, CancellationToken cancellationToken = default)
    {
        var deleted = await _dbContext.Vaccines
           .Where(i => i.Id == vaccineId)
           .ExecuteDeleteAsync(cancellationToken: cancellationToken);

        return deleted > 1;
    }

    /// <summary>
    /// Get all vaccines by person id
    /// </summary>
    /// <param name="personId">Person unique identifier</param>
    /// <param name="cancellationToken"></param>
    /// <returns>List of vaccines</returns>
    public Task<List<Vaccine>> GetAllVaccinesAsync(CancellationToken cancellationToken = default)
    {
        var vacines = _dbContext.Vaccines
            .AsNoTracking()
            .ToListAsync(cancellationToken: cancellationToken);

        return vacines;
    }

    /// <summary>
    /// Retrieves a vaccine by their unique identifier
    /// </summary>
    /// <param name="vaccineId">Vaccine unique identifier</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>Vaccine found or null if not found</returns>
    public Task<Vaccine?> GetVaccineByIdAsync(Guid vaccineId, CancellationToken cancellationToken = default)
    {
        var vaccine = _dbContext.Vaccines
            .AsNoTracking()
            .FirstOrDefaultAsync(o => o.Id == vaccineId, cancellationToken);

        return vaccine; 
    }

    /// <summary>
    /// Retrieves a vaccine by its name
    /// </summary>
    /// <param name="name"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>Retrieves vaccine if found or null if not found</returns>
    public Task<Vaccine?> GetVaccineByNameAsync(string name, CancellationToken cancellationToken = default)
    {
         var vaccine = _dbContext.Vaccines
            .AsNoTracking()
            .FirstOrDefaultAsync(o => EF.Functions.Like(o.Name, name), cancellationToken);

        return vaccine;
    }

    /// <summary>
    /// Update a Vaccine data in the repository
    /// </summary>
    /// <param name="vaccine">Vaccine object to update</param>
    /// <param name="cancellationToken">Cancellation Token</param>
    /// <returns>Vaccine updated object</returns>
    public async Task<Vaccine?> UpdateVaccineAsync(Vaccine vaccine, CancellationToken cancellationToken = default)
    {
        _dbContext.Vaccines.Update(vaccine);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return await _dbContext.Vaccines
            .FirstOrDefaultAsync(i => i.Id == vaccine.Id, cancellationToken: cancellationToken);
    }
}
