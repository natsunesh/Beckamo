<<<<<<< HEAD
﻿using System.Collections.Generic;

namespace Beckamo.Models;

public class BackupSettings
{
    public string SourceConnectionString { get; set; }   // строка подключения к PostgreSQL
    public string TargetFilePathTemplate { get; set; }   // шаблон пути для бекап‑файлов
    public List<string> IncludedDatabases { get; set; }
    public List<string> IncludedTables { get; set; }
    public bool UseEncryption { get; set; }
    public string EncryptionPassword { get; set; }
=======
﻿using System.Collections.Generic;

namespace Beckamo.Models;

public class BackupSettings
{
    public string SourceConnectionString { get; set; }   // строка подключения к PostgreSQL
    public string TargetFilePathTemplate { get; set; }   // шаблон пути для бекап‑файлов
    public List<string> IncludedDatabases { get; set; }
    public List<string> IncludedTables { get; set; }
    public bool UseEncryption { get; set; }
    public string EncryptionPassword { get; set; }
>>>>>>> 4cd8a9c83d127e7b68b0c141b233ba7e0124c7bb
}