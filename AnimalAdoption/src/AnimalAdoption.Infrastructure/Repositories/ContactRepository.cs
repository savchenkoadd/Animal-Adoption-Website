using AnimalAdoption.Core.Domain.Entities;
using AnimalAdoption.Core.Domain.RepositoryContracts;
using AnimalAdoption.Infrastructure.Db;

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

        public Task<bool> Create(ContactForm contactForm)
		{
			throw new NotImplementedException();
		}

		public Task<List<ContactForm>> GetAll()
		{
			throw new NotImplementedException();
		}

		public Task<List<ContactForm>> GetByUserId(Guid userId)
		{
			throw new NotImplementedException();
		}

		public Task<bool> Respond(Guid userId, string response)
		{
			throw new NotImplementedException();
		}
	}
}
