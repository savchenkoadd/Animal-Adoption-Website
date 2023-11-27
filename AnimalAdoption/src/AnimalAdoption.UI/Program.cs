using AnimalAdoption.Infrastructure.Db;
using Microsoft.EntityFrameworkCore;

namespace AnimalAdoption.UI
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			builder.Services.AddDbContext<ApplicationDbContext>(
					options => options.UseSqlServer(
						builder.Configuration.GetConnectionString("DefaultConnection")
						)
				); ;

			var app = builder.Build();

			app.Run();
		}
	}
}