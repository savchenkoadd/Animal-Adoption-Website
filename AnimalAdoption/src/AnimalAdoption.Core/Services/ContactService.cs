using AnimalAdoption.Core.Domain.RepositoryContracts;
using AnimalAdoption.Core.DTO;
using AnimalAdoption.Core.Helpers;
using AnimalAdoption.Core.ServiceContracts;
using AnimalAdoption.Core.Domain.Entities;

namespace AnimalAdoption.Core.Services
{
	public class ContactService : IContactService
	{
		private readonly IContactRepository _contactRepository;

        public ContactService(
				IContactRepository contactRepository
			)
        {
            _contactRepository = contactRepository;
        }

        public async Task<bool> Create(ContactFormRequest? contactFormRequest)
		{
			await ValidationHelper.ValidateObject(contactFormRequest);

			return await _contactRepository.Create(new ContactForm()
			{
				Description = contactFormRequest.Description,
				SenderEmail = contactFormRequest.SenderEmail,
				SenderId = contactFormRequest.SenderId,
				Subject = contactFormRequest.Subject,
				Id = Guid.NewGuid()
			});
		}

		public async Task<List<ContactFormResponse>> GetAll()
		{
			var responses = await _contactRepository.GetAll();

			if (responses is null)
			{
				return new List<ContactFormResponse>();
			}

			return responses.Select(temp => new ContactFormResponse()
			{
				Description = temp.Description,
				SenderEmail = temp.SenderEmail,
				Response = temp.Response,
				SenderId = temp.SenderId,
				Subject = temp.Subject,
			}).ToList();
		}

		public async Task<List<ContactFormResponse>> GetByUserId(Guid? userId)
		{
			await ValidationHelper.ValidateObject(userId);

			var responses = await _contactRepository.GetByUserId(userId);

			if (responses is null)
			{
				return new List<ContactFormResponse>();
			}

			return responses.Select(temp => new ContactFormResponse()
			{
				Description = temp.Description,
				SenderEmail = temp.SenderEmail,
				Response = temp.Response,
				SenderId = temp.SenderId,
				Subject = temp.Subject,
			}).ToList();
		}

		public async Task<bool> Respond(Guid userId, string response)
		{
			await ValidationHelper.ValidateObject(userId);
			await ValidationHelper.ValidateObject(response);

			return await _contactRepository.Respond(userId, response);
		}
	}
}
