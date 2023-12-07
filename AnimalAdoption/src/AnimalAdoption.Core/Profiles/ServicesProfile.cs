using AnimalAdoption.Core.Domain.Entities;
using AnimalAdoption.Core.DTO.AnimalProfile;
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
		}
    }
}
