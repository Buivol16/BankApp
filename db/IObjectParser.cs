namespace BankApp.db{
    public interface IObjectParser<T>
    {
        T Parse(string data);
    }
}