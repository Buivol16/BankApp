namespace BankApp.db.transaction{
    public interface ITransactionInterceptor {
        void Execute(Transaction transaction);
    }
}