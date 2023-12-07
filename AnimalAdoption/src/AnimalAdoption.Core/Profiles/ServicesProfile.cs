using AnimalAdoption.Core.Domain.Entities;
using AnimalAdoption.Core.DTO.AnimalProfile;
using AnimalAdoption.Core.DTO.ContactForm;
using AutoMapper;

namespace AnimalAdoption.Core.Profiles
{
	public class ServicesProfile : Profile
	{
        public ServicesProfile()
        {
			//source -> target
			CreateMap<AnimalProfileAddRequest, AnimalProfile>();
			CreateMap<AnimalProfile, AnimalProfileResponse>();
			CreateMap<AnimalProfileUpdateRequest, AnimalProfile>();

			CreateMap<ContactFormCreateRequest, ContactForm>();
			CreateMap<ContactForm, ContactFormResponse>();
		}
    }
}
