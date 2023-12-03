using AnimalAdoption.Core.Domain.Entities;
using AnimalAdoption.Core.Domain.RepositoryContracts;
using AnimalAdoption.Core.DTO;
using AnimalAdoption.Infrastructure.Db;
using Microsoft.EntityFrameworkCore;

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

		public async Task<bool> AddRequest(Request request)
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

		public async Task<List<Request>?> GetAllRequests()
		{
			return await _db.Requests.ToListAsync();
		}

		public async Task<Request?> GetRequest(Guid requestId)
		{
			var animalProfile = await _db.Requests.FindAsync(requestId);

			return animalProfile;
		}

		public async Task<List<Request>?> GetRequestsByUserId(Guid userId)
		{
			return await _db.Requests.Where(x => x.UserId == userId).ToListAsync();
		}

		public async Task<bool> UpdateRequest(Guid requestId, Request updateRequest)
		{
			var request = await _db.Requests.FindAsync(requestId);

			if (request is null)
			{
				return false;
			}

			request.Description = updateRequest.Description;
			request.Age = updateRequest.Age;
			request.Name = updateRequest.Name;
			request.Breed = updateRequest.Breed;
			request.ImageUrl = updateRequest.ImageUrl;
			request.Status = updateRequest.Status;
			request.AnimalId = updateRequest.AnimalId;
			request.UserId = updateRequest.UserId;

			await _db.SaveChangesAsync();
			
			return true;
		}
	}
}
