using AnimalAdoption.Core.Domain.Entities;
using AnimalAdoption.Core.Domain.RepositoryContracts;
using AnimalAdoption.Core.DTO;
using AnimalAdoption.Infrastructure.Db;
using AnimalAdoption.Infrastructure.Helpers;
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
			var rowsAffected = await OperationHelper.PerformOperationAndSaveAsync(_db, async () => await _db.Requests.AddAsync(request));

			return rowsAffected > 0;
		}

		public async Task<bool> DeleteRequest(Guid requestId)
		{
			var animalProfile = await _db.Requests.FindAsync(requestId);

			if (animalProfile is null)
			{
				return false;
			}

			var rowsAffected = await OperationHelper.PerformOperationAndSaveAsync(_db, async () => _db.Requests.Remove(animalProfile));

			return rowsAffected > 0;
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

			await CopyProperties(updateRequest, request);

			var rowsAffected = await OperationHelper.PerformOperationAndSaveAsync(_db, async () => _db.Requests.Update(request));
			
			return rowsAffected > 0;
		}

		private async Task CopyProperties(Request from, Request to)
		{
			to.Description = from.Description;
			to.Age = from.Age;
			to.Name = from.Name;
			to.Breed = from.Breed;
			to.ImageUrl = from.ImageUrl;
			to.Status = from.Status;
			to.AnimalId = from.AnimalId;
			to.UserId = from.UserId;

			await Task.CompletedTask;
		}
	}
}
