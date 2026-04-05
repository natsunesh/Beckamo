<<<<<<< HEAD
﻿using System.Threading.Tasks;

namespace Beckamo.Services;
public interface ITargetStorageService
{
    string BuildPath(string databaseName, string tableName, string template);
    Task MoveFileAsync(string sourcePath, string targetPath);
=======
﻿using System.Threading.Tasks;

namespace Beckamo.Services;
public interface ITargetStorageService
{
    string BuildPath(string databaseName, string tableName, string template);
    Task MoveFileAsync(string sourcePath, string targetPath);
>>>>>>> 4cd8a9c83d127e7b68b0c141b233ba7e0124c7bb
}