namespace OasiBank.Classes; 

public class BankTransferRequest
{
    public string ReceiverIBAN { get; set; }
    public float Amount { get; set; }
}
