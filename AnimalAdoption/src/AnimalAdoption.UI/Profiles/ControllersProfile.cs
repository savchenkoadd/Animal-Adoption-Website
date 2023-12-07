using AnimalAdoption.Core.Domain.IdentityEntities;
using AnimalAdoption.Core.DTO.AnimalProfile;
using AnimalAdoption.Core.DTO.Identity;
using AutoMapper;

namespace AnimalAdoption.UI.Profiles
{
	public class ControllersProfile : Profile
	{
        public ControllersProfile()
        {
            CreateMap<RegisterDTO, ApplicationUser>();
			CreateMap<AnimalProfileResponse, AnimalProfileUpdateRequest>();
		}
    }
}
