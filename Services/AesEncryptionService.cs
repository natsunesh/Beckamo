using BCrypt.Net;
using Beckamo.Services;
using Serilog;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Beckamo.Services;
public class AesEncryptionService : IEncryptionService
{
    private readonly ILogger _logger;

    public AesEncryptionService(ILogger logger)
    {
        _logger = logger;
    }

    public async Task<string> EncryptToFileAsync(
        byte[] data,
        string tableName,
        bool useEncryption,
        string password)
    {
        var tempPath = Path.GetTempFileName();

        if (!useEncryption)
        {
            // Просто запись в файл без шифрования
            await File.WriteAllBytesAsync(tempPath, data);
            _logger.Information("Файл {Table} записан без шифрования: {Path}", tableName, tempPath);
            return tempPath;
        }

        // BCrypt хеширует пароль → ключ для AES‑256
        var salt = BCrypt.Net.BCrypt.GenerateSalt();
        var hashedKey = BCrypt.Net.BCrypt.HashPassword(password, salt);

        // Берём первые 32 байта хешированного ключа как AES‑256 ключ
        var keyBytes = new Rfc2898DeriveBytes(hashedKey, Encoding.UTF8.GetBytes(salt)).GetBytes(32);
            

        using var aes = Aes.Create();
        aes.Key = keyBytes;
        aes.GenerateIV();

        using var fileStream = new FileStream(tempPath, FileMode.Create);
        using var cryptoStream = new CryptoStream(fileStream, aes.CreateEncryptor(), CryptoStreamMode.Write);
        await cryptoStream.WriteAsync(data, 0, data.Length);
        cryptoStream.FlushFinalBlock();

        _logger.Information("Файл {Table} зашифрован: {Path}, ключ хеширован через BCrypt.", tableName, tempPath);
        return tempPath;
    }
}