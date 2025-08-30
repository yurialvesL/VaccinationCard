using Microsoft.EntityFrameworkCore;
using VaccinationCard.Domain.Entities;
using VaccinationCard.Domain.Interfaces;
using VaccinationCard.Infrastructure.Context;

namespace VaccinationCard.Infrastructure.Repositories;


/// <summary>
/// Implementation of IPersonRepository using Entity Framework Core
/// </summary>
public class PersonRepository(ApplicationDbContext dbContext) : IPersonRepository
{
    private readonly ApplicationDbContext _dbContext = dbContext;

    /// <summary>
    /// Creates a new Person in the repository
    /// </summary>
    /// <param name="person">The person to create</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created person</returns>
    public async Task<Person> CreatePersonAsync(Person person,CancellationToken cancellationToken)
    {
        _dbContext.Persons.Add(person);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return person;
    }

    /// <summary>
    /// Retrieves a person by their unique identifier
    /// </summary>
    /// <param name="id">The unique identifier of the person</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The person if found, null otherwise</returns>
    public async Task<Person?> GetPersonByIdAsync(Guid personId, CancellationToken cancellationToken)
    {
        return await _dbContext.Persons.FirstOrDefaultAsync(o => o.Id == personId, cancellationToken);
    }

    /// <summary>
    /// Retrieves all person records from the repository
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The person if found, null otherwise</returns>
    public async Task<List<Person>> GetAllPersonAsync(CancellationToken cancellationToken)
    {
        return await _dbContext.Persons.ToListAsync(cancellationToken: cancellationToken);
    }

    /// <summary>
    /// Deletes a person by their unique identifier
    /// </summary>
    /// <param name="personId">person unique identifier</param>
    /// <param name="cancellationToken">Cancellation token</param>  
    /// <returns>True if the person was deleted, false otherwise</returns>
    public async Task<bool> DeletePersonAsync(Guid personId, CancellationToken cancellationToken)
    {
        var deleted = await _dbContext.Persons
           .Where(i => i.Id == personId)
           .ExecuteDeleteAsync(cancellationToken: cancellationToken);

        return deleted > 1;
    }

    /// <summary>
    /// Updates an existing person in the repository
    /// </summary>
    /// <param name="person">person unique identifier</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The updated person if found, null otherwise</returns>
    public async Task<Person?> UpdatePersonAsync(Person person, CancellationToken cancellationToken)
    {
        _dbContext.Persons.Update(person);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return await _dbContext.Persons
            .FirstOrDefaultAsync(i => i.Id == person.Id, cancellationToken: cancellationToken);
    }

}
