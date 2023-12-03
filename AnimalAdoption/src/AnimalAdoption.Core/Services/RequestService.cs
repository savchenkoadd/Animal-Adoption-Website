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

		public async Task<bool> AddRequest(AddRequest? request)
		{
			await ValidationHelper.ValidateObject(request);

			return await _requestRepository.AddRequest(new Domain.Entities.Request()
			{
				Age = request.Age,
				UserId = request.UserId,
				Breed = request.Breed,
				Description = request.Description,
				ImageUrl = request.ImageUrl,
				Name = request.Name,
				AnimalId = Guid.NewGuid()
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

			await _animalRepository.CreateAnimalProfile(new Domain.Entities.AnimalProfile()
			{
				Age = currentRequest.Age,
				Breed = currentRequest.Breed,
				Description = currentRequest.Description,
				ImageUrl = currentRequest.ImageUrl,
				Name = currentRequest.Name,
				Id = currentRequest.AnimalId
			});

			return true;
		}

		public async Task<List<RequestResponse>> GetRequests()
		{
			var requests = await _requestRepository.GetAllRequests();

			if (requests is null)
			{
				return new List<RequestResponse>();
			}

			return requests
				.Select(temp => new RequestResponse()
				{
					Age = temp.Age,
					Breed = temp.Breed,
					Description = temp.Description,
					ImageUrl = temp.ImageUrl,
					Name = temp.Name,
					Id = temp.AnimalId
				}).ToList();
		}

		public async Task<List<RequestResponse>> GetRequestsByUserId(Guid? userId)
		{
			await ValidationHelper.ValidateObject(userId);

			var requests = await _requestRepository.GetRequestsByUserId(userId.Value);

			return requests.Select(temp =>
				new RequestResponse()
				{
					Age = temp.Age,
					Breed = temp.Breed,
					Description = temp.Description,
					ImageUrl = temp.ImageUrl,
					Name = temp.Name,
					Id = temp.AnimalId
				}).ToList();
		}

		public async Task<bool> RejectRequest(Guid? requestId)
		{
			await ValidationHelper.ValidateObject(requestId);

			var currentRequest = await _requestRepository.GetRequest(requestId.Value);

			if (currentRequest is null)
			{
				return false;
			}

			return await _requestRepository.DeleteRequest(requestId.Value);
		}
	}
}
