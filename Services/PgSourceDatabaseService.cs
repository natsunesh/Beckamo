<<<<<<< HEAD
﻿using Beckamo.Models;
using Beckamo.Services;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Beckamo.Services;

public class PgSourceDatabaseService : ISourceDatabaseService
{
    private readonly BackupSettings _settings;

    public PgSourceDatabaseService(BackupSettings settings)
    {
        _settings = settings;
    }

    // Если includedTables == null — получаем все таблицы из public
    public async Task<List<string>> GetTablesAsync(string databaseName, List<string> includedTables)
    {
        var result = new List<string>();

        if (includedTables is not null)
        {
            result.AddRange(includedTables);
            return result;
        }

        // Если нет явного списка, берём таблицы из БД
        await using var conn = new NpgsqlConnection(_settings.SourceConnectionString);
        await conn.OpenAsync();

        const string query = "SELECT tablename FROM pg_tables WHERE schemaname = 'public';";

        using var cmd = new NpgsqlCommand(query, conn);
        await using var reader = await cmd.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            result.Add(reader.GetString("tablename"));
        }

        return result;
    }

    // Экспорт таблицы: 
    public async Task<byte[]> ExportTableAsync(string databaseName, string tableName)
    {
        var sb = new StringBuilder();

        // Стартовая часть файла: мета‑информация
        sb.AppendLine("-- START DUMP: " + tableName);
        sb.AppendLine("-- SCHEMA: " + databaseName);
        sb.AppendLine("-- TIMESTAMP: " + DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss"));
        sb.AppendLine("-- FORMAT: PostgreSQL COPY (TEXT) - tab delimited");
        sb.AppendLine();
        sb.AppendLine("BEGIN;");
        sb.AppendLine();

        var structure = await ExportTableStructureAsync(databaseName, tableName);
        sb.Append(structure.ToString());
        sb.AppendLine();

        sb.AppendLine("-- DATA: COPY table TO STDOUT");
        sb.AppendLine("-- Use this line in restore: ");
        sb.AppendLine($"-- COPY {tableName} FROM STDIN (FORMAT TEXT, DELIMITER E'\\t', NULL 'NULL');");
        sb.AppendLine();

        // Данные в отдельный .dump через COPY TO STDOUT
        string sql = sb.ToString();

        return Encoding.UTF8.GetBytes(sql);
    }

    // Экспорт только структуры таблицы
    public async Task<StringBuilder> ExportTableStructureAsync(string databaseName, string tableName)
    {
        var sb = new StringBuilder();

        await using var conn = new NpgsqlConnection(_settings.SourceConnectionString);
        await conn.OpenAsync();

        sb.AppendLine($"-- SCHEMA FOR TABLE: {tableName}");

        const string query =
            @"SELECT column_name, data_type, is_nullable
          FROM information_schema.columns
          WHERE table_schema = 'public' AND table_name = @tableName;";

        using var cmd = new NpgsqlCommand(query, conn);
        cmd.Parameters.AddWithValue("@tableName", tableName);

        await using var reader = await cmd.ExecuteReaderAsync();

        sb.AppendLine($"CREATE TABLE IF NOT EXISTS {tableName} (");

        bool first = true;
        while (await reader.ReadAsync())
        {
            if (!first)
                sb.Append(", ");

            var name = reader.GetString("column_name");
            var type = reader.GetString("data_type");
            var isNullableString = reader.GetString("is_nullable");
            var isNullable = isNullableString == "YES";  // YES/NO -> bool

            sb.Append($"    {name} {type} ");
            sb.Append(isNullable ? "NULL" : "NOT NULL");

            first = false;
        }

        sb.AppendLine(");");
        sb.AppendLine();
        sb.AppendLine("-- END SCHEMA");

        return sb;
    }

    public async Task<string> ExportTableDataToCopyAsync(string databaseName, string tableName, string dumpFilePath)
    {
        using var conn = new NpgsqlConnection(_settings.SourceConnectionString);
        await conn.OpenAsync();

        using var checkCmd = new NpgsqlCommand(
            "SELECT 1 FROM pg_tables WHERE schemaname = 'public' AND tablename = @table",
            conn);
        checkCmd.Parameters.AddWithValue("@table", tableName);
        var exists = await checkCmd.ExecuteScalarAsync();
        if (exists == null)
            throw new InvalidOperationException($"Table {tableName} not found in public schema.");

        var fullPath = Path.GetFullPath(dumpFilePath);

        if (File.Exists(fullPath))
            File.Delete(fullPath);

        Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

        await using var file = File.Create(fullPath);
        await using var writer = new StreamWriter(file, Encoding.UTF8, leaveOpen: false);

        var copyCmd = $"COPY public.{tableName} TO STDOUT (FORMAT TEXT, DELIMITER E'\\t', NULL 'NULL');";

        await using var reader = conn.BeginTextExport(copyCmd);

        string line;
        while ((line = reader.ReadLine()) != null)
        {
            await writer.WriteLineAsync(line);
        }

        return fullPath;
    }
=======
﻿using Beckamo.Models;
using Beckamo.Services;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Beckamo.Services;

public class PgSourceDatabaseService : ISourceDatabaseService
{
    private readonly BackupSettings _settings;

    public PgSourceDatabaseService(BackupSettings settings)
    {
        _settings = settings;
    }

    // Если includedTables == null — получаем все таблицы из public
    public async Task<List<string>> GetTablesAsync(string databaseName, List<string> includedTables)
    {
        var result = new List<string>();

        if (includedTables is not null)
        {
            result.AddRange(includedTables);
            return result;
        }

        // Если нет явного списка, берём таблицы из БД
        await using var conn = new NpgsqlConnection(_settings.SourceConnectionString);
        await conn.OpenAsync();

        const string query = "SELECT tablename FROM pg_tables WHERE schemaname = 'public';";

        using var cmd = new NpgsqlCommand(query, conn);
        await using var reader = await cmd.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            result.Add(reader.GetString("tablename"));
        }

        return result;
    }

    // Экспорт таблицы: 
    public async Task<byte[]> ExportTableAsync(string databaseName, string tableName)
    {
        var sb = new StringBuilder();

        sb.AppendLine("-- START DUMP: " + tableName);
        sb.AppendLine("-- SCHEMA: " + databaseName);
        sb.AppendLine("-- TIMESTAMP: " + DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss"));
        sb.AppendLine("-- This file is a simplified SQL dump (structure + data) for example.");
        sb.AppendLine();

        // Получаем структуру
        var structure = await ExportTableStructureAsync(databaseName, tableName);
        sb.Append(structure.ToString());
        sb.AppendLine();

        // Заголовок данных
        sb.AppendLine("-- DATA");
        sb.AppendLine("-- INSERT statements below (simplified for example) ");
        sb.AppendLine("-- Columns: id, name, email (пример) ");

        using var conn = new NpgsqlConnection(_settings.SourceConnectionString);
        await conn.OpenAsync();

        using var cmd = new NpgsqlCommand($"SELECT * FROM {tableName};", conn);
        await using var reader = await cmd.ExecuteReaderAsync();

        bool firstRow = true;
        while (await reader.ReadAsync())
        {
            if (!firstRow)
                sb.AppendLine(",");
            else
            {
                sb.AppendLine("INSERT INTO " + tableName + " (id, name, email) VALUES ");
                firstRow = false;
            }

            sb.Append("(");
            sb.Append(reader.GetInt32(0)); // id

            var name = reader.IsDBNull(1) ? "NULL" : '"' + reader.GetString(1).Replace("\"", "\"\"") + '"';
            var email = reader.IsDBNull(2) ? "NULL" : '"' + reader.GetString(2).Replace("\"", "\"\"") + '"';

            sb.Append(", ");
            sb.Append(name);
            sb.Append(", ");
            sb.Append(email);
            sb.Append(")");
        }

        if (!firstRow)
            sb.AppendLine(";");

        sb.AppendLine();
        sb.AppendLine("-- END DUMP");

        return Encoding.UTF8.GetBytes(sb.ToString());
    }

    // Экспорт только структуры таблицы
    public async Task<StringBuilder> ExportTableStructureAsync(string databaseName, string tableName)
    {
        var sb = new StringBuilder();

        await using var conn = new NpgsqlConnection(_settings.SourceConnectionString);
        await conn.OpenAsync();

        sb.AppendLine($"-- SCHEMA FOR TABLE: {tableName}");

        const string query =
            @"SELECT column_name, data_type, is_nullable
          FROM information_schema.columns
          WHERE table_schema = 'public' AND table_name = @tableName;";

        using var cmd = new NpgsqlCommand(query, conn);
        cmd.Parameters.AddWithValue("@tableName", tableName);

        await using var reader = await cmd.ExecuteReaderAsync();

        sb.AppendLine($"CREATE TABLE IF NOT EXISTS {tableName} (");

        bool first = true;
        while (await reader.ReadAsync())
        {
            if (!first)
                sb.Append(", ");

            var name = reader.GetString("column_name");
            var type = reader.GetString("data_type");
            var isNullableString = reader.GetString("is_nullable");
            var isNullable = isNullableString == "YES";  // YES/NO -> bool

            sb.Append($"    {name} {type} ");
            sb.Append(isNullable ? "NULL" : "NOT NULL");

            first = false;
        }

        sb.AppendLine(");");
        sb.AppendLine();
        sb.AppendLine("-- END SCHEMA");

        return sb;
    }
>>>>>>> 4cd8a9c83d127e7b68b0c141b233ba7e0124c7bb
}