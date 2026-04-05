<<<<<<< HEAD
﻿using Beckamo.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Beckamo.Infrastructure.Security;
public static class ServiceCollectionExtensions
{
    public static void ConfigureServices(this IServiceCollection services)
    {
        // Сервисы
        services
            .AddSingleton<IBackupService, BackupService>()
            .AddSingleton<IEncryptionService, AesEncryptionService>()
            .AddSingleton<ISourceDatabaseService, PgSourceDatabaseService>()
            .AddSingleton<ITargetStorageService, LocalTargetStorageService>()
            .AddSingleton<IErrorHandler, BackupErrorHandler>();

       
    }
=======
﻿using Beckamo.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Beckamo.Infrastructure.Security;
public static class ServiceCollectionExtensions
{
    public static void ConfigureServices(this IServiceCollection services)
    {
        // Сервисы
        services
            .AddSingleton<IBackupService, BackupService>()
            .AddSingleton<IEncryptionService, AesEncryptionService>()
            .AddSingleton<ISourceDatabaseService, PgSourceDatabaseService>()
            .AddSingleton<ITargetStorageService, LocalTargetStorageService>()
            .AddSingleton<IErrorHandler, BackupErrorHandler>();

       
    }
>>>>>>> 4cd8a9c83d127e7b68b0c141b233ba7e0124c7bb
}