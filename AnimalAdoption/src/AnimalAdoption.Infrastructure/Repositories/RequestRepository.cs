using AnimalAdoption.Core.Domain.Entities;
using AnimalAdoption.Core.Domain.RepositoryContracts;
using AnimalAdoption.Infrastructure.Db;

namespace AnimalAdoption.Infrastructure.Repositories
{
	public class RequestRepository : IRequestRepository
	{
		private readonly ApplicationDbContext _db;

		public RequestRepository(
				ApplicationDbContext applicationDbContext
			)
		{
			_db = applicationDbContext;
		}

		public async Task<bool> AddRequest(AnimalProfile request)
		{
			await _db.Requests.AddAsync(request);
			await _db.SaveChangesAsync();

			return true;
		}

		public async Task<bool> DeleteRequest(Guid requestId)
		{
			var animalProfile = await _db.Requests.FindAsync(requestId);

			if (animalProfile is null)
			{
				return false;
			}

			_db.Requests.Remove(animalProfile);
			await _db.SaveChangesAsync();

			return true;
		}
	}
}
