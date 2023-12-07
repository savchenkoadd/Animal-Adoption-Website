using AnimalAdoption.Core.Domain.Entities;
using AnimalAdoption.Core.DTO.AnimalProfile;
using AnimalAdoption.Core.DTO.ContactForm;
using AnimalAdoption.Core.DTO.Request;
using AutoMapper;

namespace AnimalAdoption.Core.Profiles
{
	/// <summary>
	/// AutoMapper profile for mapping between DTOs and domain entities in Animal Adoption services.
	/// </summary>
	public class ServicesProfile : Profile
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ServicesProfile"/> class.
		/// </summary>
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
