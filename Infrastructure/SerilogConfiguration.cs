using Serilog;
using Serilog.Sinks.SystemConsole;
using Serilog.Sinks.File;

namespace Beckamo.Infrastructure;

public static class SerilogConfiguration
{
    public static void ConfigureSerilog()
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Information()
            .WriteTo.Console()
            .WriteTo.File("logs/backamo.log", rollingInterval: RollingInterval.Day)
            .CreateLogger();
    }
}