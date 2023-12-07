﻿using AnimalAdoption.Core.Domain.RepositoryContracts;
using AnimalAdoption.Core.DTO.Request;
using AnimalAdoption.Core.Enums;
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
			await ValidationHelper.ValidateObjects(request);

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

		public async Task<List<RequestResponse>> GetRequestsForAdmin()
		{
			var requests = await _requestRepository.GetAllRequests();

			if (requests is null)
			{
				return new List<RequestResponse>();
			}

			return requests
				.Where(x => x.Status == RequestStatus.InProcess)
				.Select(temp => new RequestResponse()
				{
					Age = temp.Age,
					Breed = temp.Breed,
					Description = temp.Description,
					ImageUrl = temp.ImageUrl,
					Name = temp.Name,
					Id = temp.AnimalId,
					Status = temp.Status
				}).ToList();
		}

		public async Task<List<RequestResponse>> GetRequestsByUserId(Guid? userId)
		{
			await ValidationHelper.ValidateObjects(userId);

			var requests = await _requestRepository.GetRequestsByUserId(userId.Value);

			if (requests is null)
			{
				return new List<RequestResponse>();
			}

			return requests.Select(temp =>
				new RequestResponse()
				{
					Age = temp.Age,
					Breed = temp.Breed,
					Description = temp.Description,
					ImageUrl = temp.ImageUrl,
					Name = temp.Name,
					Id = temp.AnimalId,
					Status = temp.Status
				}).ToList();
		}

		public async Task<bool> ApproveRequest(Guid? requestId)
		{
			await ValidationHelper.ValidateObjects(requestId);

			var currentRequest = await _requestRepository.GetRequest(requestId.Value);

			if (currentRequest is null)
			{
				return false;
			}

			await _requestRepository.UpdateRequest(requestId.Value, new Domain.Entities.Request()
			{
				Age = currentRequest.Age,
				Breed = currentRequest.Breed,
				Description = currentRequest.Description,
				ImageUrl = currentRequest.ImageUrl,
				Name = currentRequest.Name,
				AnimalId = currentRequest.AnimalId,
				Status = RequestStatus.Approved,
				UserId = currentRequest.UserId,
			});

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

		public async Task<bool> RejectRequest(Guid? requestId)
		{
			await ValidationHelper.ValidateObjects(requestId);

			var currentRequest = await _requestRepository.GetRequest(requestId.Value);

			if (currentRequest is null)
			{
				return false;
			}

			await _requestRepository.UpdateRequest(requestId.Value, new Domain.Entities.Request()
			{
				UserId = currentRequest.UserId,
				Age = currentRequest.Age,
				Breed = currentRequest.Breed,
				Description = currentRequest.Description,
				ImageUrl = currentRequest.ImageUrl,
				Name = currentRequest.Name,
				AnimalId = currentRequest.AnimalId,
				Status = RequestStatus.Rejected
			});

			return true;
		}
	}
}
