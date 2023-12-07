using AnimalAdoption.Core.Domain.RepositoryContracts;
using AnimalAdoption.Core.Helpers;
using AnimalAdoption.Core.ServiceContracts;
using AnimalAdoption.Core.Domain.Entities;
using AnimalAdoption.Core.DTO.ContactForm;

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

        public async Task<bool> Create(ContactFormCreateRequest? contactFormRequest)
		{
			await ValidationHelper.ValidateObject(contactFormRequest);

			return await _contactRepository.Create(new ContactForm()
			{
				Description = contactFormRequest.Description,
				SenderId = contactFormRequest.SenderId,
				Subject = contactFormRequest.Subject,
				SenderEmail = contactFormRequest.SenderEmail,
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
				Id = temp.Id,
			}).ToList();
		}

		public async Task<List<ContactFormResponse>> GetByUserId(Guid? userId)
		{
			await ValidationHelper.ValidateObject(userId);

			var responses = await _contactRepository.GetByUserId(userId.Value);

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
				Id = temp.Id,
			}).ToList();
		}

		public async Task<bool> Respond(ContactFormRespondRequest? request)
		{
			await ValidationHelper.ValidateObject(request);

			return await _contactRepository.Respond(request.SenderId.Value, request.Id.Value, request.Response);
		}
	}
}
