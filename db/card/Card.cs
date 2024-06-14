using System.Text.Json;
using BankApp.db.user;

namespace BankApp.db.card
{
    public class Card
    {
        public int CardNumber { get; set; }
        public User Owner { get; set; }
        public float Money { get; set; }

        public Card(int CardNumber, User Owner, float Money){
            this.CardNumber = CardNumber;
            this.Owner = Owner;
            this.Money = Money;
        }

        public string ToJson()
        {
            JsonSerializerOptions jso = new()
            {
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            };
            return JsonSerializer.Serialize(this, GetType(), options: jso);
        }

        public override bool Equals(object? obj)
        {
            return base.Equals(obj);
        }
    }


}
