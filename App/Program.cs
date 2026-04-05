<<<<<<< HEAD
﻿using Beckamo.Infrastructure;
using Beckamo.Models;
using Beckamo.Services;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System.Text.Json;


namespace Beckamo.App;

class Program
{
    static async Task Main(string[] args)
    {
        // 1. Сервис Serilog
        SerilogConfiguration.ConfigureSerilog();

        // 2. Читаем настройки из settings.json
        var settingsPath = Path.Combine(
            Directory.GetCurrentDirectory(),
            "settings.json");

        var json = await File.ReadAllTextAsync(settingsPath);
        var settings = JsonSerializer.Deserialize<BackupSettings>(json);

        // 3. DI
        var services = new ServiceCollection();
        services.AddSingleton<ILogger>(Log.Logger);

        services.AddSingleton(settings);

        // Регистрация сервисов
        services
            .AddSingleton<IBackupService, BackupService>()
            .AddSingleton<IEncryptionService, AesEncryptionService>()
            .AddSingleton<ISourceDatabaseService, PgSourceDatabaseService>()
            .AddSingleton<ITargetStorageService, LocalTargetStorageService>()
            .AddSingleton<IErrorHandler, BackupErrorHandler>();

        var serviceProvider = services.BuildServiceProvider();

        // Запуск
        var backupService = serviceProvider.GetRequiredService<IBackupService>();
        await backupService.ExecuteBackupAsync(settings);

        Console.WriteLine("Бекап завершён. Нажмите любую клавишу...");
        Console.ReadKey();
    }
=======
﻿using Beckamo.Infrastructure;
using Beckamo.Models;
using Beckamo.Services;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System.Text.Json;


namespace Beckamo.App;

class Program
{
    static async Task Main(string[] args)
    {
        // 1. Сервис Serilog
        SerilogConfiguration.ConfigureSerilog();

        // 2. Читаем настройки из settings.json
        var settingsPath = Path.Combine(
            Directory.GetCurrentDirectory(),
            "settings.json");

        var json = await File.ReadAllTextAsync(settingsPath);
        var settings = JsonSerializer.Deserialize<BackupSettings>(json);

        // 3. DI
        var services = new ServiceCollection();
        services.AddSingleton<ILogger>(Log.Logger);

        services.AddSingleton(settings);

        // Регистрация сервисов
        services
            .AddSingleton<IBackupService, BackupService>()
            .AddSingleton<IEncryptionService, AesEncryptionService>()
            .AddSingleton<ISourceDatabaseService, PgSourceDatabaseService>()
            .AddSingleton<ITargetStorageService, LocalTargetStorageService>()
            .AddSingleton<IErrorHandler, BackupErrorHandler>();

        var serviceProvider = services.BuildServiceProvider();

        // Запуск
        var backupService = serviceProvider.GetRequiredService<IBackupService>();
        await backupService.ExecuteBackupAsync(settings);

        Console.WriteLine("Бекап завершён. Нажмите любую клавишу...");
        Console.ReadKey();
    }
>>>>>>> 4cd8a9c83d127e7b68b0c141b233ba7e0124c7bb
}