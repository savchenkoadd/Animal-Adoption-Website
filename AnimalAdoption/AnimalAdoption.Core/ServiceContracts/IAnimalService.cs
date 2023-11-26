using AnimalAdoption.Core.Domain.Entities;
using AnimalAdoption.Core.DTO;

namespace AnimalAdoption.Core.ServiceContracts
{
	public interface IAnimalService
	{
		Task<bool> CreateAnimalProfile(AnimalProfileAddRequest? animalProfile);

		Task<int> DeleteAnimalProfile(Guid? animalId);

		Task<int> UpdateAnimalProfile(Guid? id, AnimalProfileUpdateRequest? animalRequest);

		Task<IEnumerable<AnimalProfileResponse>> GetAnimalProfiles();
	}
}
