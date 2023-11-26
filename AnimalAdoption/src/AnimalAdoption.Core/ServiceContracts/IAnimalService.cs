using AnimalAdoption.Core.Domain.Entities;
using AnimalAdoption.Core.DTO;

namespace AnimalAdoption.Core.ServiceContracts
{
	public interface IAnimalService
	{
		Task<bool> CreateAnimalProfile(AnimalProfileAddRequest? animalProfileAddRequest);

		Task<int> DeleteAnimalProfile(Guid? id);

		Task<int> UpdateAnimalProfile(Guid? id, AnimalProfileUpdateRequest? animalProfileUpdateRequest);

		Task<List<AnimalProfileResponse>> GetAnimalProfiles();
	}
}
