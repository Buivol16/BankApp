using BankApp.assets;
using BankApp.db;
using BankApp.db.card;
using BankApp.db.io;
using BankApp.exceptions;

public class TransactionFacade : IDbHandler<Transaction>
{
    private readonly CardFacade cardFacade;
    public void DeleteById(int id)
    {
        var transaction = FindById(id);
        var fm = new FileManager(FileConsts.UserFilePath, true);
        var arrays = FileManager.ReadAll<Transaction>(FileConsts.TransactionFilePath);
        new FileManager(FileConsts.TransactionFilePath, false).Write("");

        foreach (var item in arrays)
        {
            if (item.Equals(transaction)) arrays.Remove(item);
        }

        foreach (var item in arrays)
        {
            fm.Write(item.ToJson() + " ,\n, ");
        }
    }

    public Transaction[] FindAll()
    {
        try
        {
            var arrays = FileManager.ReadAll<Transaction>(FileConsts.TransactionFilePath);
            return [.. arrays];
        }
        catch (Exception)
        {
            return [];
        }
    }

    public Transaction FindById(int id)
    {
        var arrays = FileManager.ReadAll<Transaction>(FileConsts.TransactionFilePath);
        if (arrays.Count != 0)
        {
            var found = arrays.Where(a => a.Id == id);
            if (found.Count() == 1) return found.First();
        }
        throw new ObjectNotFoundException("There are no transactions found");
    }

    public int FindLastIndex()
    {
        var all = FindAll();
        if (all.Length > 0)
        {
            var id = all.Last().Id;
            if (id != null) return (int)id;
        }
        return -1;
    }

    public Transaction Save(Transaction entity)
    {
        var fm = new FileManager(FileConsts.TransactionFilePath, true);
        entity.Id = FindLastIndex() + 1;
        fm.Write(entity.ToJson() + " ,\n, ");
        return entity;
    }

    public Transaction Update(Transaction entity)
    {
        var transaction = FindById((int)entity.Id);
        var fm = new FileManager(FileConsts.TransactionFilePath, true);
        var arrays = FileManager.ReadAll<Transaction>(FileConsts.TransactionFilePath);
        new FileManager(FileConsts.TransactionFilePath, false).Write("");

        foreach (var item in arrays)
        {
            if (item.Equals(transaction)) arrays[arrays.FindIndex((obj) => item.Id == obj.Id)] = entity;
        }

        foreach (var item in arrays)
        {
            fm.Write(item.ToJson() + " ,\n, ");
        }

        return entity;
    }

    public Transaction[] FindByCardNumber(int cardNumber)
    {
        return FindAll().Where<Transaction>(obj => obj.From.CardNumber == cardNumber || obj.To.CardNumber == cardNumber).ToArray();
    }
}