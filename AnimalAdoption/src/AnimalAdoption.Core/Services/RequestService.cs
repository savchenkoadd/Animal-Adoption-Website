using AnimalAdoption.Core.Domain.Entities;
using AnimalAdoption.Core.Domain.RepositoryContracts;
using AnimalAdoption.Core.DTO.Request;
using AnimalAdoption.Core.Enums;
using AnimalAdoption.Core.Helpers;
using AnimalAdoption.Core.ServiceContracts;
using AutoMapper;

namespace AnimalAdoption.Core.Services
{
    public class RequestService : IRequestService
	{
		private readonly IMapper _mapper;
		private readonly IRequestRepository _requestRepository;
		private readonly IAnimalRepository _animalRepository;

		public RequestService(
				IMapper mapper,
				IRequestRepository requestRepository,
				IAnimalRepository animalRepository
			)
		{
			_mapper = mapper;
			_requestRepository = requestRepository;
			_animalRepository = animalRepository;
		}

		public async Task<bool> AddRequest(AddRequest? addRequest)
		{
			await ValidationHelper.ValidateObjects(addRequest);

			var request = _mapper.Map<Request>(addRequest);
			request.AnimalId = Guid.NewGuid();

			return await _requestRepository.AddRequest(request);
		}

		public async Task<List<RequestResponse>> GetRequestsForAdmin()
		{
			var requests = await _requestRepository.GetAllRequests();

			if (requests is null)
			{
				return new List<RequestResponse>();
			}

			var foundRequests = requests
				.Where(x => x.Status == RequestStatus.InProcess);

			return _mapper.Map<List<RequestResponse>>(foundRequests);
		}

		public async Task<List<RequestResponse>> GetRequestsByUserId(Guid? userId)
		{
			await ValidationHelper.ValidateObjects(userId);

			var requests = await _requestRepository.GetRequestsByUserId(userId.Value);

			if (requests is null)
			{
				return new List<RequestResponse>();
			}

			return _mapper.Map<List<RequestResponse>>(requests);
		}

		public async Task<bool> ApproveRequest(Guid? requestId)
		{
			await ValidationHelper.ValidateObjects(requestId);

			var currentRequest = await _requestRepository.GetRequest(requestId!.Value);

			if (currentRequest is null)
			{
				return false;
			}

			var updatedRequest = _mapper.Map<Request>(currentRequest);
			updatedRequest.Status = RequestStatus.Approved;

			await _requestRepository.UpdateRequest(requestId.Value, updatedRequest);

			var animalProfile = _mapper.Map<AnimalProfile>(currentRequest);

			await _animalRepository.CreateAnimalProfile(animalProfile);

			return true;
		}

		public async Task<bool> RejectRequest(Guid? requestId)
		{
			await ValidationHelper.ValidateObjects(requestId);

			var currentRequest = await _requestRepository.GetRequest(requestId!.Value);

			if (currentRequest is null)
			{
				return false;
			}

			var updatedRequest = _mapper.Map<Request>(currentRequest);
			updatedRequest.Status = RequestStatus.Rejected;

			await _requestRepository.UpdateRequest(requestId.Value, updatedRequest);

			return true;
		}
	}
}
