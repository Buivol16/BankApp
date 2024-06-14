using BankApp.db.card;

public class transactionParser
{
    public static Transaction TransactionDTOToTransaction(TransactionDTO transactionDTO)
    {
        var fromCard = CardParser.CardDTOToCard(transactionDTO.From);
        var toCard = CardParser.CardDTOToCard(transactionDTO.To);

        return new Transaction(null, fromCard, toCard, DateTime.Now, transactionDTO.Summ);
    }

    public static TransactionDTO TransactionToTransactionDTO(Transaction transaction){
        var fromCard = CardParser.CardToCardDTO(transaction.From);
        var toCard = CardParser.CardToCardDTO(transaction.To);

        return new TransactionDTO(fromCard, toCard, transaction.DateTime, transaction.Summ);
    }
}