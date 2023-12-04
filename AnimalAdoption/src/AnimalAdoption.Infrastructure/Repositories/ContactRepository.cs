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
			var form = await _db.ContactForms.Where(temp => temp.SenderId == userId && temp.Id == formId).FirstOrDefaultAsync();

			if (form is null)
			{
				return false;
			}

			form.Response = response;

			_db.Update(form);
			await _db.SaveChangesAsync();

			return true;
		}
	}
}
