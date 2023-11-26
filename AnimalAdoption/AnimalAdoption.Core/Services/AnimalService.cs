using AnimalAdoption.Core.Domain.RepositoryContracts;
using AnimalAdoption.Core.DTO;
using AnimalAdoption.Core.ServiceContracts;
using AnimalAdoption.Core.Domain.Entities;

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

        public Task<bool> CreateAnimalProfile(AnimalProfileAddRequest? animalProfileRequest)
		{
			if (animalProfileRequest is null)
			{
				throw new ArgumentNullException();
			}

			var animalProfile1 = new AnimalProfile

			_animalRepository.CreateAnimalProfile(animalProfileRequest);
		}

		public Task<int> DeleteAnimalProfile(Guid? animalId)
		{
			throw new NotImplementedException();
		}

		public Task<List<AnimalProfileResponse>> GetAnimalProfiles()
		{
			throw new NotImplementedException();
		}

		public Task<int> UpdateAnimalProfile(AnimalProfileUpdateRequest? animalId)
		{
			throw new NotImplementedException();
		}
	}
}
