using AnimalAdoption.Core.Domain.RepositoryContracts;
using AnimalAdoption.Core.DTO;
using AnimalAdoption.Core.ServiceContracts;
using AnimalAdoption.Core.Domain.Entities;
using AnimalAdoption.Core.Helpers;

namespace AnimalAdoption.Core.Services
{
	public class AnimalService : IAnimalService
	{
		private IAnimalRepository _animalRepository;

        public AnimalService(
				IAnimalRepository animalRepository
			)
        {
            _animalRepository = animalRepository;
        }

		public async Task<bool> CreateAnimalProfile(AnimalProfileAddRequest? animalProfileAddRequest)
		{
			await ValidationHelper.ValidateRequest(animalProfileAddRequest);

			var animalProfile = new AnimalProfile()
			{
				Id = Guid.NewGuid(),
				Name = animalProfileAddRequest!.Name,
				Age = animalProfileAddRequest!.Age,
				Breed = animalProfileAddRequest!.Breed,
				Description = animalProfileAddRequest!.Description,
				ImageUrl = animalProfileAddRequest!.ImageUrl,
			};

			return await _animalRepository.CreateAnimalProfile(animalProfile);
		}

		public async Task<int> DeleteAnimalProfile(Guid? id)
		{
			if (id is null)
			{
				throw new ArgumentNullException();
			}

			return await _animalRepository.DeleteAnimalProfile(id.Value);
		}

		public async Task<List<AnimalProfileResponse>> GetAnimalProfiles()
		{
			var profiles = await _animalRepository.GetAnimalProfiles();

			if (profiles is null)
			{
				return new List<AnimalProfileResponse>();
			}

			return profiles.ToList();
		}

		public async Task<int> UpdateAnimalProfile(Guid? id, AnimalProfileUpdateRequest? animalProfileUpdateRequest)
		{
			if (id is null)
			{
				throw new ArgumentNullException();
			}

			await ValidationHelper.ValidateRequest(animalProfileUpdateRequest);

			return await _animalRepository.UpdateAnimalProfile(id.Value, animalProfileUpdateRequest!);
		}
	}
}
