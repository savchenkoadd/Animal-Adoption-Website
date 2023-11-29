using AnimalAdoption.UI.StartupExtensions;

namespace AnimalAdoption.UI
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			builder.Services.ConfigureServices(builder.Configuration);

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