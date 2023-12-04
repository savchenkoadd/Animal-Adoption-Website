using AnimalAdoption.Core.Domain.Entities;
using AnimalAdoption.Core.DTO;

namespace AnimalAdoption.Core.ServiceContracts
{
	public interface IContactService
	{
		Task<bool> Create(ContactFormRequest contactFormRequest);

		Task<List<ContactFormResponse>> GetAll();

		Task<List<ContactFormResponse>> GetByUserId(Guid userId);

		Task<bool> Respond(Guid userId, string response);
	}
}
