using AnimalAdoption.Core.Domain.RepositoryContracts;
using AnimalAdoption.Core.DTO;
using AnimalAdoption.Core.ServiceContracts;
using AnimalAdoption.Core.Domain.Entities;
using AnimalAdoption.Core.Helpers;

namespace AnimalAdoption.Core.Services
{
	public class AnimalService : IAnimalService
	{
		private readonly IAnimalRepository _animalRepository;

        public AnimalService(
				IAnimalRepository animalRepository
			)
        {
            _animalRepository = animalRepository;
        }

		public async Task<bool> CreateAnimalProfile(AnimalProfileAddRequest? animalProfileAddRequest)
		{
			await ValidationHelper.ValidateObject(animalProfileAddRequest);

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

		public async Task<AnimalProfileResponse> GetAnimalProfileById(Guid? id)
		{
			if (id is null)
			{
				throw new ArgumentNullException();
			}

			var animalProfile = await _animalRepository.GetAnimalProfileById(id.Value);

			if (animalProfile is null)
			{
				throw new ArgumentException();
			}

			return new AnimalProfileResponse()
			{
				Id = animalProfile.Id,
				Name = animalProfile.Name,
				Age = animalProfile.Age,
				Breed = animalProfile.Breed,
				Description = animalProfile.Description,
				ImageUrl = animalProfile.ImageUrl
			};
		}

		public async Task<List<AnimalProfileResponse>> GetAnimalProfiles()
		{
			var profiles = await _animalRepository.GetAnimalProfiles();

			if (profiles is null)
			{
				return new List<AnimalProfileResponse>();
			}

			return profiles.Select(x => new AnimalProfileResponse() {
				Id = x.Id,
				Name = x.Name,
				Age = x.Age,
				Breed = x.Breed,
				Description = x.Description,
				ImageUrl = x.ImageUrl,
			}).ToList();
		}

		public async Task<List<AnimalProfileResponse>> SearchByName(string? name)
		{
			await ValidationHelper.ValidateObject(name);

			var results = await _animalRepository.SearchByName(name);

			if (results is not null)
			{
				return results.Select(temp => new AnimalProfileResponse()
				{
					Id = temp.Id,
					Name = temp.Name,
					Age = temp.Age,
					Breed = temp.Breed,
					Description = temp.Description,
					ImageUrl = temp.ImageUrl
				}).ToList();
			}

			return new List<AnimalProfileResponse>();
		}

		public async Task<int> UpdateAnimalProfile(Guid? id, AnimalProfileUpdateRequest? animalProfileUpdateRequest)
		{
			if (id is null)
			{
				throw new ArgumentNullException();
			}

			await ValidationHelper.ValidateObject(animalProfileUpdateRequest);

			return await _animalRepository.UpdateAnimalProfile(id.Value, new AnimalProfile()
			{
				ImageUrl = animalProfileUpdateRequest?.ImageUrl,
				Breed = animalProfileUpdateRequest?.Breed,
				Name = animalProfileUpdateRequest?.Name,
				Description = animalProfileUpdateRequest?.Description,
				Age = animalProfileUpdateRequest?.Age
			});
		}
	}
}
