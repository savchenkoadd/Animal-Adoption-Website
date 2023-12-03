using AnimalAdoption.Core.DTO;

namespace AnimalAdoption.Core.ServiceContracts
{
	public interface IRequestService
	{
		Task<bool> AddRequest(AddRequest? request);

		Task<bool> RejectRequest(Guid? requestId);

		Task<bool> ApproveRequest(Guid? requestId);

		Task<List<RequestResponse>> GetRequestsForAdmin();

		Task<List<RequestResponse>> GetRequestsByUserId(Guid? userId);
	}
}
