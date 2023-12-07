using AnimalAdoption.Core.Domain.RepositoryContracts;
using AnimalAdoption.Core.Helpers;
using AnimalAdoption.Core.ServiceContracts;
using AnimalAdoption.Core.Domain.Entities;
using AnimalAdoption.Core.DTO.ContactForm;
using AutoMapper;

namespace AnimalAdoption.Core.Services
{
    public class ContactService : IContactService
	{
		private readonly IMapper _mapper;
		private readonly IContactRepository _contactRepository;

        public ContactService(
				IMapper mapper,
				IContactRepository contactRepository
			)
        {
			_mapper = mapper;
            _contactRepository = contactRepository;
        }

        public async Task<bool> Create(ContactFormCreateRequest? contactFormRequest)
		{
			await ValidationHelper.ValidateObjects(contactFormRequest);

			var form = _mapper.Map<ContactForm>(contactFormRequest);
			form.Id = Guid.NewGuid();

			return await _contactRepository.Create(form);
		}

		public async Task<List<ContactFormResponse>> GetAll()
		{
			var responses = await _contactRepository.GetAll();

			if (responses is null)
			{
				return new List<ContactFormResponse>();
			}

			return _mapper.Map<List<ContactFormResponse>>(responses);
		}

		public async Task<List<ContactFormResponse>> GetByUserId(Guid? userId)
		{
			await ValidationHelper.ValidateObjects(userId);

			var responses = await _contactRepository.GetByUserId(userId!.Value);

			if (responses is null)
			{
				return new List<ContactFormResponse>();
			}

			return _mapper.Map<List<ContactFormResponse>>(responses);
		}

		public async Task<bool> Respond(ContactFormRespondRequest? request)
		{
			await ValidationHelper.ValidateObjects(request);

			return await _contactRepository.Respond(request!.SenderId!.Value, request.Id!.Value, request.Response!);
		}
	}
}
