namespace nil70n.AspNet.Helpers.FromClaim.Tests.Api.Models;

public class Person
{
  public Person(string email, string name, string lastName)
  {
    Email = email;
    Name = name;
    LastName = lastName;
  }

  public string Email { get; }
  public string Name { get; }
  public string LastName { get; }
}