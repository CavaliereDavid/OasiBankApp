using Newtonsoft.Json;
using OasiBank.Classes;

namespace OasiBank.Model;

public class RegistrationModel
{
    public string name { get; set; }
    public string surname { get; set; }
    public string email { get; set; }
    public string password { get; set; }

}
