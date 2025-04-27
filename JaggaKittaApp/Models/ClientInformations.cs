namespace JaggaKittaApp.Models;

public class ClientInformations
{
    public ClientInformations()
    {
        Address = new Address();
    }
    public string FirstName { get; set; }
    public string MiddleName { get; set; }
    public string LastName { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public Address Address { get; set; }
}

public class Address
{
    public string Street { get; set; }
    public string Ward { get; set; }
    public string District { get; set; }
    public string City { get; set; }
}