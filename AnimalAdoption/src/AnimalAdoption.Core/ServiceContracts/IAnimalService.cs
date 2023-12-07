using AnimalAdoption.Core.DTO.AnimalProfile;

namespace AnimalAdoption.Core.ServiceContracts
{
	/// <summary>
	/// Service contract defining operations for managing animal profiles.
	/// </summary>
	public interface IAnimalService
	{
		/// <summary>
		/// Creates a new animal profile.
		/// </summary>
		/// <param name="animalProfileAddRequest">The request containing data for creating the animal profile.</param>
		/// <returns>A task representing the asynchronous operation. Returns true if the operation is successful.</returns>
		Task<bool> CreateAnimalProfile(AnimalProfileAddRequest? animalProfileAddRequest);

		/// <summary>
		/// Deletes an animal profile by its unique identifier.
		/// </summary>
		/// <param name="id">The unique identifier of the animal profile to be deleted.</param>
		/// <returns>A task representing the asynchronous operation. Returns the number of affected rows.</returns>
		Task<int> DeleteAnimalProfile(Guid? id);

		/// <summary>
		/// Updates an existing animal profile.
		/// </summary>
		/// <param name="id">The unique identifier of the animal profile to be updated.</param>
		/// <param name="animalProfileUpdateRequest">The request containing updated data for the animal profile.</param>
		/// <returns>A task representing the asynchronous operation. Returns the number of affected rows.</returns>
		Task<int> UpdateAnimalProfile(Guid? id, AnimalProfileUpdateRequest? animalProfileUpdateRequest);

		/// <summary>
		/// Retrieves a list of all animal profiles.
		/// </summary>
		/// <returns>A task representing the asynchronous operation. Returns a list of animal profiles.</returns>
		Task<List<AnimalProfileResponse>> GetAnimalProfiles();

		/// <summary>
		/// Retrieves an animal profile by its unique identifier.
		/// </summary>
		/// <param name="id">The unique identifier of the animal profile to retrieve.</param>
		/// <returns>A task representing the asynchronous operation. Returns the requested animal profile.</returns>
		Task<AnimalProfileResponse> GetAnimalProfileById(Guid? id);

		/// <summary>
		/// Searches for animal profiles by name.
		/// </summary>
		/// <param name="name">The name to use as the search criteria.</param>
		/// <returns>A task representing the asynchronous operation. Returns a list of matching animal profiles.</returns>
		Task<List<AnimalProfileResponse>> SearchByName(string? name);
	}
}
