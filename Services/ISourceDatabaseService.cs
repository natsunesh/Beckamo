
﻿using System.Collections.Generic;
using System.Threading.Tasks;

namespace Beckamo.Services;

public interface ISourceDatabaseService
{
    Task<List<string>> GetTablesAsync(string databaseName, List<string> includedTables);
    Task<byte[]> ExportTableAsync(string databaseName, string tableName);
    Task<string> ExportTableDataToCopyAsync(string databaseName, string tableName, string dumpFilePath);

}
