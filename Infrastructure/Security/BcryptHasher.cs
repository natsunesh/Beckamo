<<<<<<< HEAD
﻿using BCrypt.Net;

namespace Beckamo.Infrastructure.Security;

public static class BcryptHasher
{
    public static string Hash(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password, 12);
    }

    public static bool Verify(string password, string hashedPassword)
    {
        return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
    }
=======
﻿using BCrypt.Net;

namespace Beckamo.Infrastructure.Security;

public static class BcryptHasher
{
    public static string Hash(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password, 12);
    }

    public static bool Verify(string password, string hashedPassword)
    {
        return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
    }
>>>>>>> 4cd8a9c83d127e7b68b0c141b233ba7e0124c7bb
}