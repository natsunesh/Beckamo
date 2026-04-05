using System.Threading.Tasks;

namespace Beckamo.Services;
public interface ITargetStorageService
{
    string BuildPath(string databaseName, string tableName, string template);
    Task MoveFileAsync(string sourcePath, string targetPath);

}
