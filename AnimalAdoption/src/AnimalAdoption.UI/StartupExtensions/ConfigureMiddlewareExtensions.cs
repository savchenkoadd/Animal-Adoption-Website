using AnimalAdoption.UI.Controllers;
using AnimalAdoption.UI.Middleware;

namespace AnimalAdoption.UI.StartupExtensions
{
	public static class ConfigureMiddlewareExtensions
	{
		private const string ERROR_ACTION_HANDLER_PATH = nameof(ErrorController.Error);

		public static void ConfigureMiddleware(this WebApplication app)
		{
			ConfigureExceptionHandling(app);

			app.UseHsts();
			app.UseHttpsRedirection();

			app.UseStaticFiles();

			app.UseRouting();
			app.UseAuthentication();
			app.UseAuthorization();

			app.MapControllers();
		}

		private static void ConfigureExceptionHandling(WebApplication app)
		{
			if (app.Environment.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler($"/{ERROR_ACTION_HANDLER_PATH}");
				app.UseExceptionHandlingMiddleware();
			}
		}
	}
}
