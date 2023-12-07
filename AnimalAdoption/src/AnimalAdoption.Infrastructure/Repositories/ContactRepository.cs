using AnimalAdoption.Core.Domain.Entities;
using AnimalAdoption.Core.Domain.RepositoryContracts;
using AnimalAdoption.Infrastructure.Db;
using AnimalAdoption.Infrastructure.Helpers;
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
			var changed = await OperationHelper.PerformOperationAndSaveAsync(_db,
				async () => await _db.ContactForms.AddAsync(contactForm));

			return changed > 0;
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
			var contactForm = await _db.ContactForms
				.FirstOrDefaultAsync(temp => temp.SenderId == userId && temp.Id == formId);

			if (contactForm == null)
			{
				return false; 
			}

			contactForm.Response = response;

			var rowsAffected = await OperationHelper.PerformOperationAndSaveAsync(_db, async () =>
			{
				_db.ContactForms.Update(contactForm);
			});

			return rowsAffected > 0;
		}

		private async Task<ContactForm?> FindForm(params Guid[] ids)
		{
			var form = await _db.ContactForms.FindAsync(ids);

			return form;
		}
	}
}
