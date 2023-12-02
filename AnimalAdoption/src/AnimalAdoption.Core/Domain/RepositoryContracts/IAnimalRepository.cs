using AnimalAdoption.Core.Domain.Entities;
using AnimalAdoption.Core.DTO;

namespace AnimalAdoption.Core.Domain.RepositoryContracts
{
	public interface IAnimalRepository
	{
		Task<bool> CreateAnimalProfile(AnimalProfile animalProfile);

		Task<int> DeleteAnimalProfile(Guid animalId);

		Task<int> UpdateAnimalProfile(Guid id, AnimalProfile animalRequest);

		Task<List<AnimalProfile>?> GetAnimalProfiles();

		Task<AnimalProfile?> GetAnimalProfileById(Guid id);

		Task<List<AnimalProfile>?> SearchByName(string name);
	}
}
