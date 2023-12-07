using AnimalAdoption.Core.Domain.Entities;

namespace AnimalAdoption.Core.Domain.RepositoryContracts
{
	/// <summary>
	/// Interface defining operations for managing animal profiles in a repository.
	/// </summary>
	public interface IAnimalRepository
	{
		/// <summary>
		/// Creates a new animal profile.
		/// </summary>
		/// <param name="animalProfile">The animal profile to be created.</param>
		/// <returns>A task representing the asynchronous operation. Returns true if the operation is successful.</returns>
		Task<bool> CreateAnimalProfile(AnimalProfile animalProfile);

		/// <summary>
		/// Deletes an animal profile by its unique identifier.
		/// </summary>
		/// <param name="animalId">The unique identifier of the animal profile to be deleted.</param>
		/// <returns>A task representing the asynchronous operation. Returns the number of affected rows.</returns>
		Task<int> DeleteAnimalProfile(Guid animalId);

		/// <summary>
		/// Updates an existing animal profile.
		/// </summary>
		/// <param name="id">The unique identifier of the animal profile to be updated.</param>
		/// <param name="animalRequest">The updated animal profile data.</param>
		/// <returns>A task representing the asynchronous operation. Returns the number of affected rows.</returns>
		Task<int> UpdateAnimalProfile(Guid id, AnimalProfile animalRequest);

		/// <summary>
		/// Retrieves a list of all animal profiles.
		/// </summary>
		/// <returns>A task representing the asynchronous operation. Returns a list of animal profiles or null if none are found.</returns>
		Task<List<AnimalProfile>?> GetAnimalProfiles();

		/// <summary>
		/// Retrieves an animal profile by its unique identifier.
		/// </summary>
		/// <param name="id">The unique identifier of the animal profile to retrieve.</param>
		/// <returns>A task representing the asynchronous operation. Returns the requested animal profile or null if not found.</returns>
		Task<AnimalProfile?> GetAnimalProfileById(Guid id);

		/// <summary>
		/// Searches for animal profiles by name.
		/// </summary>
		/// <param name="name">The name to use as the search criteria.</param>
		/// <returns>A task representing the asynchronous operation. Returns a list of matching animal profiles or null if none are found.</returns>
		Task<List<AnimalProfile>?> SearchByName(string name);
	}
}
