
using Beckamo.Models;
using Beckamo.Services;
using Serilog;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Beckamo.Services;

public class BackupService : IBackupService
{
    private readonly ISourceDatabaseService _sourceDb;
    private readonly ITargetStorageService _target;
    private readonly IEncryptionService _encryption;
    private readonly ILogger _logger;

    public BackupService(
        ISourceDatabaseService sourceDb,
        ITargetStorageService target,
        IEncryptionService encryption,
        ILogger logger)
    {
        _sourceDb = sourceDb;
        _target = target;
        _encryption = encryption;
        _logger = logger;
    }

    public async Task ExecuteBackupAsync(BackupSettings settings)
    {
        foreach (var dbName in settings.IncludedDatabases)
        {
            try
            {
                _logger.Information("Начинаем бекап базы {Database}", dbName);

                var tables = await _sourceDb.GetTablesAsync(dbName, settings.IncludedTables);

                foreach (var table in tables)
                {
                    _logger.Information("Экспортируем таблицу {Table} из базы {Database}", table, dbName);

                    var data = await _sourceDb.ExportTableAsync(dbName, table);

                    var encryptedPath = await _encryption.EncryptToFileAsync(
                        data,
                        table,
                        settings.UseEncryption,
                        settings.EncryptionPassword
                    );

                    var targetPath = _target.BuildPath(dbName, table, settings.TargetFilePathTemplate);
                    await _target.MoveFileAsync(encryptedPath, targetPath);

                    _logger.Information("Бекап таблицы {Table} сохранён в {Path}", table, targetPath);
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Ошибка бекапа базы {Database}", dbName);
            }
        }
    }
}