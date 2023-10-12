namespace OasiBank.Classes;

public class RegistrationResponse
{
    public User user { get; set; }
    public DateTime openingDate { get; set; }
    public string iban { get; set; }
    public string id { get; set; }
}
