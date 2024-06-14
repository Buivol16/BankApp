using BankApp.db.user;

public interface ITokenGenerator
{
    void GenerateToken(ref byte[] token);
    User DecyphToken(byte[] token);
}