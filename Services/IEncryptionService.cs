<<<<<<< HEAD
﻿using System.Threading.Tasks;

namespace Beckamo.Services;
public interface IEncryptionService
{
    Task<string> EncryptToFileAsync(
        byte[] data,
        string tableName,
        bool useEncryption,
        string password);
=======
﻿using System.Threading.Tasks;

namespace Beckamo.Services;
public interface IEncryptionService
{
    Task<string> EncryptToFileAsync(
        byte[] data,
        string tableName,
        bool useEncryption,
        string password);
>>>>>>> 4cd8a9c83d127e7b68b0c141b233ba7e0124c7bb
}