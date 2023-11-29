using AnimalAdoption.UI.StartupExtensions;
using Serilog;

namespace AnimalAdoption.UI
{
	public partial class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			Startup.ConfigureBuilder(builder);

			var app = builder.Build();

			Startup.ConfigureApp(app);

			app.Run();
		}
	}
}