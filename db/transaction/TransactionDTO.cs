using System.Text.Json;
using BankApp.db.card;

public class TransactionDTO
{
    public CardDTO From { get; set; }
    public CardDTO To { get; set; }
    public DateTime DateTime { get; set; }
    public float Summ { get; set; }

    public TransactionDTO(CardDTO from, CardDTO to, DateTime dateTime, float summ)
    {
        From = from;
        To = to;
        DateTime = dateTime;
        Summ = summ;
    }

    public override string ToString()
    {
            return $"From: {From.CardNumber}\nTo: {To.CardNumber}\nSumm: {Summ}\nDate time: {DateTime.ToString("dd-MM-yyyy HH:mm:ss")}\n";
    }
}
