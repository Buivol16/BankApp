public interface IMenuInterceptor
{
    void AboutMe(byte[] token);
    void MakeTransaction(byte[] token);
    void TransactionHistory(byte[] token);
    void Logout(ref byte[] token);
    void BankAccount(byte[] token);
}
