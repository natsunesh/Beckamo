using Beckamo.Services;
using Serilog;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Beckamo.Services;

public class LocalTargetStorageService : ITargetStorageService
{
    private readonly ILogger _logger;

    public LocalTargetStorageService(ILogger logger)
    {
        _logger = logger;
    }

    public string BuildPath(string databaseName, string tableName, string template)
    {
        var fileName = template
            .Replace("{db}", databaseName)
            .Replace("{table}", tableName)
            .Replace("{timestamp}", DateTime.UtcNow.ToString("yyyyMMdd_HHmmss"));

        var dir = Path.GetDirectoryName(fileName);
        if (!Directory.Exists(dir))
            Directory.CreateDirectory(dir);

        return fileName;
    }

    public async Task MoveFileAsync(string sourcePath, string targetPath)
    {
        if (File.Exists(sourcePath))
        {
            await using var source = File.OpenRead(sourcePath);
            await using var dest = File.Create(targetPath);
            await source.CopyToAsync(dest);
            _logger.Information("Файл перенесён: {source} -> {target}", sourcePath, targetPath);
        }
        else
        {
            _logger.Warning("Файл не найден для переноса: {source}", sourcePath);
        }
    }
}