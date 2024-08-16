namespace MauiAccelerator.Features.Profile.Models;

public class UserDto
{
    public long Id { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string MaidenName { get; set; }

    public long Age { get; set; }

    public string Gender { get; set; }

    public string Email { get; set; }

    public string Phone { get; set; }

    public string Username { get; set; }

    public string Password { get; set; }

    public string BirthDate { get; set; }

    public string Image { get; set; }

    public string BloodGroup { get; set; }

    public double Height { get; set; }

    public double Weight { get; set; }

    public string EyeColor { get; set; }

    public Hair Hair { get; set; }

    public string Ip { get; set; }

    public Address Address { get; set; }

    public string MacAddress { get; set; }

    public string University { get; set; }

    public Bank Bank { get; set; }

    public Company Company { get; set; }

    public string Ein { get; set; }

    public string Ssn { get; set; }

    public string UserAgent { get; set; }

    public Crypto Crypto { get; set; }

    public string Role { get; set; }
}

public class Address
{
    public string AddressAddress { get; set; }

    public string City { get; set; }

    public string State { get; set; }

    public string StateCode { get; set; }

    public string PostalCode { get; set; }

    public Coordinates Coordinates { get; set; }

    public string Country { get; set; }
}

public class Coordinates
{
    public double Lat { get; set; }

    public double Lng { get; set; }
}

public class Bank
{
    public string CardExpire { get; set; }

    public string CardNumber { get; set; }

    public string CardType { get; set; }

    public string Currency { get; set; }

    public string Iban { get; set; }
}

public class Company
{
    public string Department { get; set; }

    public string Name { get; set; }

    public string Title { get; set; }

    public Address Address { get; set; }
}

public class Crypto
{
    public string Coin { get; set; }

    public string Wallet { get; set; }

    public string Network { get; set; }
}

public class Hair
{
    public string Color { get; set; }

    public string Type { get; set; }
}