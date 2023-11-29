using Serilog;

namespace AnimalAdoption.UI.StartupExtensions
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
