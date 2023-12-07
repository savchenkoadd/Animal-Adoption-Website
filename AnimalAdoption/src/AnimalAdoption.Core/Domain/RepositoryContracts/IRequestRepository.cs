using AnimalAdoption.Core.Domain.Entities;

namespace AnimalAdoption.Core.Domain.RepositoryContracts
{
	public interface IRequestRepository
	{
		Task<bool> AddRequest(Request request);

		Task<bool> DeleteRequest(Guid requestId);

		Task<Request?> GetRequest(Guid requestId);

		Task<List<Request>?> GetAllRequests();

		Task<List<Request>?> GetRequestsByUserId(Guid userId);

		Task<bool> UpdateRequest(Guid requestId, Request updateRequest);
	}
}
