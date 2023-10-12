namespace OasiBank.Classes;

public class BankTransferResponse
{
    public int Amount { get; set; }
    public float Balance { get; set; }
    public DateTime Date { get; set; }
    public Sender Sender { get; set; }
    public Receiver Receiver { get; set; }
    public string TransactionCategory { get; set; }
    public string BankAccountID { get; set; }
    public string ID { get; set; }
}
