﻿using AnimalAdoption.Core.Domain.Entities;
using AnimalAdoption.Core.Domain.RepositoryContracts;
using AnimalAdoption.Infrastructure.Db;
using AnimalAdoption.Infrastructure.Helpers;
using Microsoft.EntityFrameworkCore;

namespace AnimalAdoption.Infrastructure.Repositories
{
	public class AnimalRepository : IAnimalRepository
	{
		private readonly ApplicationDbContext _db;

		public AnimalRepository(
				ApplicationDbContext dbContext
			)
		{
			_db = dbContext;
		}

		public async Task<bool> CreateAnimalProfile(AnimalProfile animalProfile)
		{
			return (await OperationHelper.PerformOperationAndSaveAsync(_db,
				async () =>
				await _db.AnimalProfiles.AddAsync(animalProfile))) > 0;
		}

		public async Task<int> DeleteAnimalProfile(Guid animalId)
		{
			var animalProfile = await FindProfile(animalId);

			if (animalProfile == null)
			{
				return 0;
			}

			return await OperationHelper.PerformOperationAndSaveAsync(_db,
				async () =>
				_db.AnimalProfiles.Remove(animalProfile));
		}

		public async Task<AnimalProfile?> GetAnimalProfileById(Guid id)
		{
			return await FindProfile(id);
		}

		public async Task<List<AnimalProfile>?> GetAnimalProfiles()
		{
			return await _db.AnimalProfiles.ToListAsync();
		}

		public async Task<List<AnimalProfile>?> SearchByName(string name)
		{
			return await _db.AnimalProfiles
				.Where(profile => profile.Name.Contains(name))
				.ToListAsync();
		}

		public async Task<int> UpdateAnimalProfile(Guid id, AnimalProfile animalRequest)
		{
			var existingAnimalProfile = await FindProfile(id);

			if (existingAnimalProfile == null)
			{
				return 0;
			}

			await CopyHelper.CopyAnimalProfileFields(animalRequest, existingAnimalProfile);

			await MarkEntityState(existingAnimalProfile, EntityState.Modified);

			return await _db.SaveChangesAsync();
		}

		private async Task MarkEntityState(AnimalProfile animalProfile, EntityState entityState)
		{
			_db.Entry(animalProfile).State = entityState;

			await Task.CompletedTask;
		}

		private async Task<AnimalProfile?> FindProfile(Guid guid)
		{
			var profile = await _db.AnimalProfiles.FindAsync(guid);

			return profile;
		}
	}
}
