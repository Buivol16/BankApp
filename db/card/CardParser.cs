using BankApp.db.io;
using BankApp.db.user;

namespace BankApp.db.card
{
    public class CardParser
    {
        private static readonly UserFacade userFacade = new();

        public static CardDTO CardToCardDTO(Card card)
        {
            return new CardDTO(card.CardNumber, card.Owner.Login, card.Money);
        }

        public static Card CardDTOToCard(CardDTO cardDTO)
        {
            var owner = userFacade.FindByLogin(cardDTO.OwnerLogin);
            return new Card(cardDTO.CardNumber, owner, cardDTO.Money);
        }
    }
}