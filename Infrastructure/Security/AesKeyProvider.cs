<<<<<<< HEAD
﻿using System.Security.Cryptography;
using System.Text;

namespace Beckamo.Infrastructure.Security;

public class AesKeyProvider
{
    public static byte[] GetKeyFromPassword(string password, string salt)
    {
        return new Rfc2898DeriveBytes(
            password,
            Encoding.UTF8.GetBytes(salt))
            .GetBytes(32);
    }

   
=======
﻿using System.Security.Cryptography;
using System.Text;

namespace Beckamo.Infrastructure.Security;

public class AesKeyProvider
{
    public static byte[] GetKeyFromPassword(string password, string salt)
    {
        return new Rfc2898DeriveBytes(
            password,
            Encoding.UTF8.GetBytes(salt))
            .GetBytes(32);
    }

   
>>>>>>> 4cd8a9c83d127e7b68b0c141b233ba7e0124c7bb
}