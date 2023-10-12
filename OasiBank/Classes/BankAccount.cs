namespace OasiBank.Classes;

public class BankAccount
{
    public User user { get; set; }
    public DateTime openingDate { get; set; }
    public string iban { get; set; }
    public string id { get; set; }
}
