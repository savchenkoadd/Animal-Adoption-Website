using Serilog;

namespace AnimalAdoption.UI.StartupExtensions.Configuration
{
    public static class SerilogConfigurator
    {
        public static IHostBuilder ConfigureSerilog(this IHostBuilder hostBuilder)
        {
            hostBuilder.UseSerilog((context, services, loggerConfiguration) =>
            {
                loggerConfiguration
                    .ReadFrom
                    .Configuration(context.Configuration)
                    .ReadFrom
                    .Services(services);
            });

            return hostBuilder;
        }
    }
}
