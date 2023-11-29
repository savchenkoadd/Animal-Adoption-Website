﻿namespace AnimalAdoption.UI.StartupExtensions
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

            services.AddHttpLogging(options =>
            {
                options.LoggingFields = Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.RequestProperties | Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.ResponsePropertiesAndHeaders;
            });
        }
	}
}
