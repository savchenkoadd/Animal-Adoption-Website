using AnimalAdoption.Core.Domain.Entities;
using AnimalAdoption.Core.Domain.RepositoryContracts;
using AnimalAdoption.Infrastructure.Db;
using Microsoft.EntityFrameworkCore;

namespace AnimalAdoption.Infrastructure.Repositories
{
	public class ContactRepository : IContactRepository
	{
		private readonly ApplicationDbContext _db;

		public ContactRepository(
			ApplicationDbContext applicationDbContext
			)
		{
			_db = applicationDbContext;
		}

		public async Task<bool> Create(ContactForm contactForm)
		{
			await _db.ContactForms.AddAsync(contactForm);
			await _db.SaveChangesAsync();

			return true;
		}

		public async Task<List<ContactForm>?> GetAll()
		{
			return await _db.ContactForms.ToListAsync();
		}

		public async Task<List<ContactForm>> GetByUserId(Guid userId)
		{
			return await _db.ContactForms.Where(temp => temp.SenderId == userId).ToListAsync();
		}

		public async Task<bool> Respond(Guid userId, Guid formId, string response)
		{
			var match = await _db.ContactForms.FindAsync(userId, formId);

			if (match is null)
			{
				return false;
			}

			match.Response = response;

			_db.ContactForms.Update(match);
			await _db.SaveChangesAsync();

			return true;
		}
	}
}
