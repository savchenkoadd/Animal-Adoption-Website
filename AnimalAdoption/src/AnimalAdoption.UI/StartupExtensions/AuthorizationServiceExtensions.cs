using Microsoft.AspNetCore.Authorization;

namespace AnimalAdoption.UI.StartupExtensions
{
	public static class AuthorizationServiceExtensions
	{
		public static void AddAuthorizationServices(this IServiceCollection services)
		{
			services.AddAuthorization(options =>
			{
				options.FallbackPolicy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
			});

			services.ConfigureApplicationCookie(options =>
			{
				options.LoginPath = "/Login";
			});
		}
	}
}
