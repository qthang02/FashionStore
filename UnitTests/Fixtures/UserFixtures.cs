using API.Models;
using Bogus;

namespace UnitTests.Fixtures;

public static class UserFixtures
{
    public static List<User> GetUsersTest()
    {
        // using Bogus to generate fake data for testing purposes 
        var address = new Faker<Address>()
            .RuleFor(a => a.Street, f => f.Address.StreetAddress())
            .RuleFor(a => a.City, f => f.Address.City())
            .RuleFor(a => a.ZipCode, f => f.Address.ZipCode());
        
        
        var users = new Faker<User>()
            .RuleFor(u => u.Id, f => f.Random.Int())
            .RuleFor(u => u.Name, f => f.Name.FullName())
            .RuleFor(u => u.Email, f => f.Internet.Email())
            .RuleFor(u => u.Address,() => address)
            .Generate(10);
        
        
        return users;
    }
}