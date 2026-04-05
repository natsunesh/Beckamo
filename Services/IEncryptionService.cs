using System.Threading.Tasks;

namespace Beckamo.Services;
public interface IEncryptionService
{
    Task<string> EncryptToFileAsync(
        byte[] data,
        string tableName,
        bool useEncryption,
        string password);

}
