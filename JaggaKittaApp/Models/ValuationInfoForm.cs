namespace JaggaKittaApp.Models;

public class ValuationInfoForm
{
    public ValuationInfoForm()
    {
        BankingDetail = new();
        ClientInformations = new();
        PropertyInformation = new();
    }

    public BankingDetails BankingDetail { get; set; } = new();
    public List<ClientInformations> ClientInformations { get; set; }= new();
    public List<PropertyInformations> PropertyInformation { get; set; }= new();
}