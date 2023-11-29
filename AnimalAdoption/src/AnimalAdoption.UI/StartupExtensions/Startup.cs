using AnimalAdoption.UI.StartupExtensions.Configuration;
using Serilog;

namespace AnimalAdoption.UI.StartupExtensions
{
    public static class Startup
    {
        public static void ConfigureBuilder(WebApplicationBuilder builder)
        {
            builder.Host.ConfigureSerilog();

            builder.Services.ConfigureServices(builder.Configuration);
        }

        public static void ConfigureApp(WebApplication app)
        {
            app.UseSerilogRequestLogging();
            app.ConfigureMiddleware();
        }
    }
}
