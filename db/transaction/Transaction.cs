using System.Text.Json;
using BankApp.db.card;

public class Transaction
{
    public int? Id { get; set; }
    public Card From { get; set; }
    public Card To { get; set; }
    public DateTime DateTime { get; set; }
    public float Summ { get; set; }

    public Transaction(int? id, Card from, Card to, DateTime dateTime, float summ)
    {
        Id = id;
        From = from;
        To = to;
        DateTime = dateTime;
        Summ = summ;
    }

    public string ToJson()
    {
        JsonSerializerOptions jso = new();
        jso.Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
        return JsonSerializer.Serialize(this, GetType(), options: jso);
    }
}
