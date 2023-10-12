using OasiBank.Classes;

namespace OasiBank.Model;

public class HomePageModel
{
    public User user { get; set; }
    public string iban { get; set; }
    public List<Transaction> transactions { get; set; }

    public HomePageModel()
    {
        user = new User();
        iban = string.Empty;
        transactions = new List<Transaction>();
    }
}
