namespace JaggaKittaApp.Models;

public class PropertyInformations
{
    public PropertyInformations()
    {
        BuildingInformation = new BuildingInformations();
    }
    public string Type { get; set; }
    public string AddressAsPerLalpurja { get; set; }
    public string CurrentAddress { get; set; }
    public string CasdastralMapSheetNumber { get; set; }
    public string PlotNumber { get; set; }
    public string PlotArea { get; set; }
    public BuildingInformations BuildingInformation { get; set; }
}

public class BuildingInformations
{
    public string TypeOfBuilding { get; set; }
    public DateTime? NaksaPassDate { get; set; }
    public string CertificateType { get; set; }
    public int AgeOfBuilding { get; set; }
    
}