
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
                    try
                    {
                        _logger.Information("Экспортируем таблицу {Table} из базы {Database}", table, dbName);

                        var sql = await _sourceDb.ExportTableAsync(dbName, table);
                        var encryptedSqlPath = await _encryption.EncryptToFileAsync(
                            sql,
                            $"{table}.sql",
                            settings.UseEncryption,
                            settings.EncryptionPassword
                        );

                        var tempDumpPath = Path.GetTempFileName();
                        await _sourceDb.ExportTableDataToCopyAsync(dbName, table, tempDumpPath);

                        var data = await File.ReadAllBytesAsync(tempDumpPath);
                        var encryptedDumpPath = await _encryption.EncryptToFileAsync(
                            data,
                            $"{table}.dump",
                            settings.UseEncryption,
                            settings.EncryptionPassword
                        );

                        var sqlTargetPath = _target.BuildPath(dbName, table, "{db}/{table}.sql.enc");
                        var dumpTargetPath = _target.BuildPath(dbName, table, "{db}/{table}.dump");

                        await _target.MoveFileAsync(encryptedSqlPath, sqlTargetPath);
                        await _target.MoveFileAsync(encryptedDumpPath, dumpTargetPath);

                        _logger.Information("Бекап таблицы {Table} сохранён в {SqlPath} и {DumpPath}", table, sqlTargetPath, dumpTargetPath);
                    }
                    catch (Exception ex)
                    {
                        _logger.Error(ex, "Ошибка бекапа таблицы {Table} в базе {Database}", table, dbName);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Ошибка бекапа базы {Database}", dbName);
            }
        }
    }
}   