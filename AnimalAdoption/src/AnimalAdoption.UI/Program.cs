using AnimalAdoption.Core.Domain.RepositoryContracts;
using AnimalAdoption.Core.ServiceContracts;
using AnimalAdoption.Core.Services;
using AnimalAdoption.Infrastructure.Db;
using AnimalAdoption.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AnimalAdoption.UI
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			builder.Services.AddControllersWithViews();

			builder.Services.AddScoped<IAnimalRepository, AnimalRepository>();
			builder.Services.AddScoped<IAnimalService, AnimalService>();

			builder.Services.AddDbContext<ApplicationDbContext>(
					options => options.UseSqlServer(
						builder.Configuration.GetConnectionString("DefaultConnection")
						)
				); ;

			var app = builder.Build();

			if (app.Environment.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseStaticFiles();
			app.UseRouting();
			app.MapControllers();

			app.Run();
		}
	}
}