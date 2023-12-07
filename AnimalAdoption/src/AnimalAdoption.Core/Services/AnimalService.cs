using AnimalAdoption.Core.Domain.RepositoryContracts;
using AnimalAdoption.Core.ServiceContracts;
using AnimalAdoption.Core.Domain.Entities;
using AnimalAdoption.Core.Helpers;
using AnimalAdoption.Core.DTO.AnimalProfile;
using AutoMapper;

namespace AnimalAdoption.Core.Services
{
	public class AnimalService : IAnimalService
	{
		private readonly IMapper _mapper;
		private readonly IAnimalRepository _animalRepository;

		public AnimalService(
				IMapper mapper,
				IAnimalRepository animalRepository
			)
		{
			_mapper = mapper;
			_animalRepository = animalRepository;
		}

		public async Task<bool> CreateAnimalProfile(AnimalProfileAddRequest? animalProfileAddRequest)
		{
			await ValidationHelper.ValidateObjects(animalProfileAddRequest);

			var animalProfile = _mapper.Map<AnimalProfile>(animalProfileAddRequest);

			animalProfile.Id = Guid.NewGuid();

			return await _animalRepository.CreateAnimalProfile(animalProfile);
		}

		public async Task<int> DeleteAnimalProfile(Guid? id)
		{
			await ValidationHelper.ValidateObjects(id);

			return await _animalRepository.DeleteAnimalProfile(id.Value);
		}

		public async Task<AnimalProfileResponse> GetAnimalProfileById(Guid? id)
		{
			await ValidationHelper.ValidateObjects(id);

			var animalProfile = await _animalRepository.GetAnimalProfileById(id.Value);

			if (animalProfile is null)
			{
				throw new ArgumentException();
			}

			return _mapper.Map<AnimalProfileResponse>(animalProfile);
		}

		public async Task<List<AnimalProfileResponse>> GetAnimalProfiles()
		{
			var profiles = await _animalRepository.GetAnimalProfiles();

			if (profiles is null)
			{
				return new List<AnimalProfileResponse>();
			}

			return _mapper.Map<List<AnimalProfileResponse>>(profiles);
		}

		public async Task<List<AnimalProfileResponse>> SearchByName(string? name)
		{
			await ValidationHelper.ValidateObjects(name);

			var results = await _animalRepository.SearchByName(name);

			if (results is not null)
			{
				return _mapper.Map<List<AnimalProfileResponse>>(results);
			}

			return new List<AnimalProfileResponse>();
		}

		public async Task<int> UpdateAnimalProfile(Guid? id, AnimalProfileUpdateRequest? animalProfileUpdateRequest)
		{
			await ValidationHelper.ValidateObjects(id, animalProfileUpdateRequest);

			return await _animalRepository.UpdateAnimalProfile(id.Value, _mapper.Map<AnimalProfile>(animalProfileUpdateRequest));
		}
	}
}
