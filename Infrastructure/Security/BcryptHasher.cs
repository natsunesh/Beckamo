using BCrypt.Net;

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

}
