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
			var rowsAffected = await AddAndSaveAsync(contactForm);

			return rowsAffected > 0;
		}

		public async Task<List<ContactForm>?> GetAll()
		{
			return await _db.ContactForms.ToListAsync();
		}

		public async Task<List<ContactForm>?> GetByUserId(Guid userId)
		{
			return await _db.ContactForms.Where(temp => temp.SenderId == userId).ToListAsync();
		}

		public async Task<bool> Respond(Guid userId, Guid formId, string response)
		{
			var contactForm = await FindContactForm(userId, formId);

			if (contactForm == null)
			{
				return false; 
			}

			contactForm.Response = response;

			var rowsAffected = await UpdateAndSaveAsync(contactForm);

			return rowsAffected > 0;
		}

		#region Private Methods

		private async Task<int> AddAndSaveAsync(ContactForm form)
		{
			return await OperationHelper.PerformOperationAndSaveAsync(_db,
				async () => await _db.ContactForms.AddAsync(form));
		}

		private async Task<int> UpdateAndSaveAsync(ContactForm form)
		{
			return await OperationHelper.PerformOperationAndSaveAsync(_db, async () =>
			{
				_db.ContactForms.Update(form);
			}); ;
		} 

		private async Task<ContactForm?> FindContactForm(Guid userId, Guid formId)
		{
			return await _db.ContactForms
				.FirstOrDefaultAsync(temp => temp.SenderId == userId && temp.Id == formId);
		}

		#endregion
	}
}
