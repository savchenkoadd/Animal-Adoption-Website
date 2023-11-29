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


			//Enable Identity
			builder.Services.AddIdentity<ApplicationUser, ApplicationRole>()
				.AddEntityFrameworkStores<ApplicationDbContext>()
				.AddDefaultTokenProviders()
				.AddUserStore<UserStore<ApplicationUser, ApplicationRole, ApplicationDbContext, Guid>>()
				.AddRoleStore<RoleStore<ApplicationRole, ApplicationDbContext, Guid>>();

			builder.Services.AddAuthorization(options =>
			{
				options.FallbackPolicy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
			});

			builder.Services.ConfigureApplicationCookie(options =>
			{
				options.LoginPath = "/Login";
			});

			var app = builder.Build();

			if (app.Environment.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseHsts();
			app.UseHttpsRedirection();

			app.UseStaticFiles();

			app.UseRouting();
			app.UseAuthentication(); //Reading Identity Cookie
			app.UseAuthorization();

			app.MapControllers();

			app.Run();
		}
	}
}