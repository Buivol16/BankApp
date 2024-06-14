using BankApp.db.card;
using BankApp.db.io;
using BankApp.db.user;
using static BankApp.assets.ConsoleHelper;

public class BankMenuInterceptor : IMenuInterceptor
{
    private readonly ITokenGenerator tokenGenerator = new TokenGeneratorImpl();
    private readonly CardFacade cardFacade = new();
    private readonly TransactionHandler transactionHandler = new();

    public void AboutMe(byte[] token)
    {
        var user = GetUser(token);
        var dto = UserParser.UserToUserDTO(user);
        WriteLine(dto.ToString());
    }

    public void BankAccount(byte[] token)
    {
        var user = GetUser(token);
        var card = cardFacade.FindByOwner(user);
        var cardDto = CardParser.CardToCardDTO(card);
        WriteLine(cardDto.ToString());
    }

    public void Logout(ref byte[] token)
    {
        token = null;
    }


    public void MakeTransaction(byte[] token)
    {
        transactionHandler.MakeTransaction(token);
    }

    public void TransactionHistory(byte[] token)
    {
        transactionHandler.History(token);
    }

    private User GetUser(byte[] token)
    {
        return tokenGenerator.DecyphToken(token);
    }
}