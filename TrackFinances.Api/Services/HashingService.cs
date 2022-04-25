using Isopoh.Cryptography.Argon2;

namespace TrackFinances.Api.Services;

public class HashingService : IHashingService
{
    public string HashPassword(string password) => 
        Argon2.Hash(password);

    public bool VerifyPassword(string passwordHash, string password) =>
        Argon2.Verify(passwordHash, password);
}
