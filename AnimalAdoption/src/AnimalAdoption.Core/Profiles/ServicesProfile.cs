using AnimalAdoption.Core.Domain.Entities;
using AnimalAdoption.Core.DTO.AnimalProfile;
using AnimalAdoption.Core.DTO.ContactForm;
using AnimalAdoption.Core.DTO.Request;
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

			CreateMap<AddRequest, Request>();
			CreateMap<Request, RequestResponse>()
				.ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.AnimalId));
			CreateMap<Request, AnimalProfile>()
			.ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.AnimalId));
			CreateMap<Request, Request>();
		}
    }
}
