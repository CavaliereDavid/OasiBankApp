namespace OasiBank.Classes
{
    public class Transaction
    {
        public int amount { get; set; }
        public int balance { get; set; }
        public DateTime date { get; set; }
        public BankAccount sender { get; set; }
        public BankAccount receiver { get; set; }
        public string transactionCategory { get; set; }
        public string bankAccountID { get; set; }
        public string id { get; set; }
    }
}
