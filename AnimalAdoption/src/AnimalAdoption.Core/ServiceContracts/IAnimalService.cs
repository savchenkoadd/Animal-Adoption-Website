using AnimalAdoption.Core.DTO.AnimalProfile;

namespace AnimalAdoption.Core.ServiceContracts
{
    public interface IAnimalService
	{
		Task<bool> CreateAnimalProfile(AnimalProfileAddRequest? animalProfileAddRequest);

		Task<int> DeleteAnimalProfile(Guid? id);

		Task<int> UpdateAnimalProfile(Guid? id, AnimalProfileUpdateRequest? animalProfileUpdateRequest);

		Task<List<AnimalProfileResponse>> GetAnimalProfiles();

		Task<AnimalProfileResponse> GetAnimalProfileById(Guid? id);

		Task<List<AnimalProfileResponse>> SearchByName(string? name);
	}
}
