using AnimalAdoption.Core.Domain.RepositoryContracts;
using AnimalAdoption.Core.ServiceContracts;
using AnimalAdoption.Core.Services;
using AnimalAdoption.Infrastructure.Repositories;

namespace AnimalAdoption.UI.StartupExtensions.Configuration
{
    public static class AnimalExtensions
    {
        public static void AddServicesScoped(this IServiceCollection services)
        {
            services.AddScoped<IAnimalService, AnimalService>();
            services.AddScoped<IRequestService, RequestService>();
			services.AddScoped<IContactService, ContactService>();
		}

        public static void AddRepositoriesScoped(this IServiceCollection services)
        {
            services.AddScoped<IAnimalRepository, AnimalRepository>();
			services.AddScoped<IRequestRepository, RequestRepository>();
			services.AddScoped<IContactRepository, ContactRepository>();
		}
    }
}
