using AnimalAdoption.Core.DTO;

namespace AnimalAdoption.Core.ServiceContracts
{
	public interface IRequestService
	{
		Task<bool> AddRequest(AnimalProfileAddRequest? animalProfileAddRequest);

		Task<bool> RejectRequest(Guid? requestId);

		Task<bool> ApproveRequest(Guid? requestId);
	}
}
