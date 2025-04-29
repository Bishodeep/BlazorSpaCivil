namespace JaggaKittaApp.Models;

public class PropertyInformations
{
    public PropertyInformations()
    {
        BuildingInformation = new BuildingInformation();
    }
    public string Type { get; set; }
    public string AddressAsPerLalpurja { get; set; }
    public string CurrentAddress { get; set; }
    public string CasdastralMapSheetNumber { get; set; }
    public string PlotNumber { get; set; }
    public string PlotArea { get; set; }
    public BuildingInformation BuildingInformation { get; set; }
    public PhysicalFacilities PhysicalFacilities { get; set; }
}

public class BuildingInformation
{
    public string TypeOfBuilding { get; set; }
    public DateTime? NaksaPassDate { get; set; }
    public string CertificateType { get; set; }
    public int AgeOfBuilding { get; set; }
    
}

public class PhysicalFacilities()
{
    public bool ElectricityAvailable { get; set; }
    public bool WaterSupplyAvailable { get; set; }
    public bool SewerAvailable { get; set; }
    public bool Telephone { get; set; }
    public bool Internet { get; set; }
    public string RoadType { get; set; }
    public decimal WidthOfRoad { get; set; }
    public string Remarks { get; set; }
}