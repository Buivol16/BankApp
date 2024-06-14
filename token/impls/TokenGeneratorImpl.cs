

using System.Text.Json;
using BankApp.db.user;

using static System.Text.Encoding;
using static BankApp.crypts.Cryptographer.AES256;

class TokenGeneratorImpl : ITokenGenerator
{

    byte[] key = UTF8.GetBytes("hello");
    byte[] iv = UTF8.GetBytes("world");

    public User DecyphToken(byte[] token)
    {
        GenerateKeyIV(ref key, ref iv);
        var json = DecryptString(token, key, iv);
        var user = JsonSerializer.Deserialize<User>(json);
        return user;
    }

    public byte[] GenerateToken(string str)
    {
        GenerateKeyIV(ref key, ref iv);
        return EncryptString(str, key, iv);
    }
}

