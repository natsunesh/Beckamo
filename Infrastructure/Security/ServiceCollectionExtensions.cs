using Beckamo.Services;
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

}
