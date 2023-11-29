using AnimalAdoption.Core.Domain.IdentityEntities;
using AnimalAdoption.Core.Domain.RepositoryContracts;
using AnimalAdoption.Core.ServiceContracts;
using AnimalAdoption.Core.Services;
using AnimalAdoption.Infrastructure.Db;
using AnimalAdoption.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AnimalAdoption.UI.StartupExtensions
{
	public static class ConfigureServicesExtension
	{
		public static void ConfigureServices(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddControllersWithViews();

			services.AddAnimalServicesScoped();
			services.AddAnimalRepositoriesScoped();
			services.AddDbContextServices(configuration);
			services.AddIdentityServices();
			services.AddAuthorizationServices();
		}
	}
}
