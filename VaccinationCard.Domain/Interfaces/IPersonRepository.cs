using VaccinationCard.Domain.Entities;

namespace VaccinationCard.Domain.Interfaces;


/// <summary>
/// Repository interface for Person entity operations
/// </summary>
public interface IPersonRepository
{
    /// <summary>
    /// Creates a new person in the repository
    /// </summary>
    /// <param name="person">The person to create</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created person</returns>
    Task<Person> CreatePersonAsync(Person person, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves a person by their unique identifier
    /// </summary>
    /// <param name="personId">The unique identifier of the person</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The person if found, null otherwise</returns>
    Task<Person?> GetPersonByIdAsync(Guid personId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves a paginated list of persons from the repository
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns></returns>
    Task<List<Person>> GetAllPersonAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes a person from the repository
    /// </summary>
    /// <param name="personId">The unique identifier of the user to delete</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if the user was deleted, false if not found</returns>
    Task<bool> DeletePersonAsync(Guid personId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates an existing person in the repository
    /// </summary>
    /// <param name="person">The person to update</param>
    /// <param name="cancellationToken">Cancellation token</param>  
    /// <returns>The updated person if successful, null if the person does not exist</returns>
    Task<Person?> UpdatePersonAsync(Person person, CancellationToken cancellationToken = default);
}
