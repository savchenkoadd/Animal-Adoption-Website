using AnimalAdoption.Core.Domain.Entities;
using AnimalAdoption.Core.Domain.RepositoryContracts;
using AnimalAdoption.Infrastructure.Db;
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
			await _db.AnimalProfiles.AddAsync(animalProfile);
			await _db.SaveChangesAsync();

			return true;
		}

		public async Task<int> DeleteAnimalProfile(Guid animalId)
		{
			var animalProfile = await _db.AnimalProfiles.FindAsync(animalId);

			if (animalProfile == null)
			{
				return 0;
			}

			_db.AnimalProfiles.Remove(animalProfile);

			return await _db.SaveChangesAsync();
		}

		public async Task<List<AnimalProfile>?> GetAnimalProfiles()
		{
			return await _db.AnimalProfiles.ToListAsync();
		}

		public async Task<int> UpdateAnimalProfile(Guid id, AnimalProfile animalRequest)
		{
			var existingAnimalProfile = await _db.AnimalProfiles.FindAsync(id);

			if (existingAnimalProfile == null)
			{
				return 0;
			}

			existingAnimalProfile.Name = animalRequest.Name;
			existingAnimalProfile.Age = animalRequest.Age;
			existingAnimalProfile.Description = animalRequest.Description;
			existingAnimalProfile.Breed = animalRequest.Breed;
			existingAnimalProfile.ImageUrl = animalRequest.ImageUrl;
			
			_db.Entry(existingAnimalProfile).State = EntityState.Modified;

			return await _db.SaveChangesAsync();
		}
	}
}
