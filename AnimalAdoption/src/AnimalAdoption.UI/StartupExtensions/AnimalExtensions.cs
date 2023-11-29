using AnimalAdoption.Core.Domain.RepositoryContracts;
using AnimalAdoption.Core.ServiceContracts;
using AnimalAdoption.Core.Services;
using AnimalAdoption.Infrastructure.Repositories;

namespace AnimalAdoption.UI.StartupExtensions
{
	public static class AnimalExtensions
	{
		public static void AddAnimalServicesScoped(this IServiceCollection services)
		{
			services.AddScoped<IAnimalService, AnimalService>();
		}

		public static void AddAnimalRepositoriesScoped(this IServiceCollection services)
		{
			services.AddScoped<IAnimalRepository, AnimalRepository>();
		}
	}
}
