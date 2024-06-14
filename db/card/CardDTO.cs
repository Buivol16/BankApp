using BankApp.db.user;

namespace BankApp.db.card
{
    public class CardDTO
    {
        public int CardNumber { get; set; }
        public string OwnerLogin { get; set; }
        public float Money { get; set; }

        public CardDTO(int CardNumber, string OwnerLogin, float Money)
        {
            this.CardNumber = CardNumber;
            this.OwnerLogin = OwnerLogin;
            this.Money = Money;
        }

        public override string ToString()
        {
            return $"Numer karty: {CardNumber}\nLogin właściciela: {OwnerLogin}\nŚrodki: {Money}\n";
        }

    }
}