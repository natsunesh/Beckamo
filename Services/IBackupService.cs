using System.Threading.Tasks;
using Beckamo.Models;

namespace Beckamo.Services;

public interface IBackupService
{
    Task ExecuteBackupAsync(BackupSettings settings);

}
