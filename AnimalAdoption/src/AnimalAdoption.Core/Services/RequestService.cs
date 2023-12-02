using AnimalAdoption.Core.Domain.RepositoryContracts;
using AnimalAdoption.Core.DTO;
using AnimalAdoption.Core.Helpers;
using AnimalAdoption.Core.ServiceContracts;

namespace AnimalAdoption.Core.Services
{
	public class RequestService : IRequestService
	{
		private readonly IRequestRepository _requestRepository;
		private readonly IAnimalRepository _animalRepository;

        public RequestService(
				IRequestRepository requestRepository,
				IAnimalRepository animalRepository
			)
        {
            _requestRepository = requestRepository;
			_animalRepository = animalRepository;
        }

        public async Task<bool> AddRequest(AnimalProfileAddRequest? animalProfileAddRequest)
		{
			await ValidationHelper.ValidateObject(animalProfileAddRequest);

			return await _requestRepository.AddRequest(new Domain.Entities.AnimalProfile()
			{
				Age = animalProfileAddRequest.Age,
				Breed = animalProfileAddRequest.Breed,
				Description = animalProfileAddRequest.Description,
				ImageUrl = animalProfileAddRequest.ImageUrl,
				Name = animalProfileAddRequest.Name,
				Id = Guid.NewGuid()
			});
		}

		public async Task<bool> ApproveRequest(Guid? requestId)
		{
			await ValidationHelper.ValidateObject(requestId);

			var currentRequest = await _requestRepository.GetRequest(requestId.Value);

			if (currentRequest is null)
			{
				return false;
			}

			await _requestRepository.DeleteRequest(requestId.Value);

			await _animalRepository.CreateAnimalProfile(currentRequest);

			return true;
		}

		public async Task<bool> RejectRequest(Guid? requestId)
		{
			await ValidationHelper.ValidateObject(requestId);

			var currentRequest = _requestRepository.GetRequest(requestId.Value);

			if (currentRequest is null)
			{
				return false;
			}

			return await _requestRepository.DeleteRequest(requestId.Value);
		}
	}
}
