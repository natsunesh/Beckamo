<<<<<<< HEAD
﻿using Serilog;
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
=======
﻿using Serilog;
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
>>>>>>> 4cd8a9c83d127e7b68b0c141b233ba7e0124c7bb
}