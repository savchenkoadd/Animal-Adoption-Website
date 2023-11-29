namespace AnimalAdoption.UI.StartupExtensions
{
	public static class ConfigureMiddlewareExtensions
	{
		public static void ConfigureMiddleware(this WebApplication app)
		{
			EnableDeveloperExceptionPageIfNeeded(app);

			app.UseHsts();
			app.UseHttpsRedirection();

			app.UseStaticFiles();

			app.UseRouting();
			app.UseAuthentication();
			app.UseAuthorization();

			app.MapControllers();
		}

		private static void EnableDeveloperExceptionPageIfNeeded(WebApplication app)
		{
			if (app.Environment.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
		}
	}
}
