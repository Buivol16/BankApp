using System.Transactions;
using BankApp.db.card;
using BankApp.db.io;
using BankApp.exceptions;
using static BankApp.assets.ConsoleHelper;


public class TransactionHandler : ITransactionHandler
{

    private readonly ITokenGenerator tokenGenerator = new TokenGeneratorImpl();
    private readonly TransactionFacade transactionFacade = new();
    private readonly CardFacade cardFacade = new();

    public void MakeTransaction(byte[] token)
    {
        var i = 0;

        int toCardNumber = 0;
        float summ = 0;

        while (true)
        {
            try
            {
                switch (i)
                {
                    case 0:
                        WriteLine("Numer karty:");
                        toCardNumber = TypeInt();
                        i = 1;
                        continue;
                    case 1:
                        WriteLine("Kwota:");
                        summ = TypeFloat();
                        i = 2;
                        break;
                }
            }
            catch
            {
                continue;
            }
            break;
        }
        try
        {
            var toCard = cardFacade.FindByCardNumber(toCardNumber);
            if (toCard.Money <= summ) throw new NotEnoughMoneyException("You have not enough money");
            var fromCard = cardFacade.FindByOwner(tokenGenerator.DecyphToken(token));
            var toCardDTO = CardParser.CardToCardDTO(toCard);
            var tokenCardDTO = CardParser.CardToCardDTO(fromCard);
            var transaction = new TransactionDTO(tokenCardDTO, toCardDTO, DateTime.Now, summ);
            toCard.Money += summ;
            fromCard.Money -= summ;
            cardFacade.Update(fromCard);
            cardFacade.Update(toCard);
            transactionFacade.Save(transactionParser.TransactionDTOToTransaction(transaction));
        }
        catch (ObjectNotFoundException)
        {
            WriteLine("Nie prawidłowy numer karty");
        }
        catch (NotEnoughMoneyException)
        {
            WriteLine("Masz nie dostatnio środków no końcie");
        }

    }

    public void History(byte[] token)
    {
        var user = tokenGenerator.DecyphToken(token);
        var cardOwner = cardFacade.FindByOwner(user);
        var history = transactionFacade.FindByCardNumber(cardOwner.CardNumber);
        foreach (var obj in history)
        {
            var transactionDTO = transactionParser.TransactionToTransactionDTO(obj);
            Console.WriteLine(transactionDTO.ToString());
        }
    }
}
