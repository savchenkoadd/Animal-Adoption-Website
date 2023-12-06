using AnimalAdoption.Core.Domain.Entities;
using AnimalAdoption.Core.DTO;

namespace AnimalAdoption.Core.ServiceContracts
{
	public interface IContactService
	{
		Task<bool> Create(ContactFormCreateRequest? contactFormRequest);

		Task<List<ContactFormResponse>> GetAll();

		Task<List<ContactFormResponse>> GetByUserId(Guid? userId);

		Task<bool> Respond(ContactFormRespondRequest? request);
	}
}
