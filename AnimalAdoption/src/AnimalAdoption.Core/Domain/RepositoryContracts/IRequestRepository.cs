using AnimalAdoption.Core.Domain.Entities;

namespace AnimalAdoption.Core.Domain.RepositoryContracts
{
	public interface IRequestRepository
	{
		Task<bool> AddRequest(AnimalProfile request);

		Task<bool> DeleteRequest(Guid requestId);
	}
}
