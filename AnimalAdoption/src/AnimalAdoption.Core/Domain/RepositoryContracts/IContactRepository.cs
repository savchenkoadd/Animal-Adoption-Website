using AnimalAdoption.Core.Domain.Entities;

namespace AnimalAdoption.Core.Domain.RepositoryContracts
{
	public interface IContactRepository
	{
		Task<bool> Create(ContactForm contactForm);

		Task<List<ContactForm>> GetAll();

		Task<List<ContactForm>> GetByUserId(Guid userId);

		Task<bool> Respond(Guid userId, string response);
	}
}
