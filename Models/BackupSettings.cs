using System.Collections.Generic;

namespace Beckamo.Models;

public class BackupSettings
{
    public string SourceConnectionString { get; set; }   // строка подключения к PostgreSQL
    public string TargetFilePathTemplate { get; set; }   // шаблон пути для бекап‑файлов
    public List<string> IncludedDatabases { get; set; }
    public List<string> IncludedTables { get; set; }
    public bool UseEncryption { get; set; }
    public string EncryptionPassword { get; set; }
}