# AspNet.Helpers.FromClaim

## Description
Adds the "FromClaim" attribute to easily get values from claims in ASP.NET controller methods.

## Configuring
After installing the package, change the Startup class adding the required factory like in the example below:

```c#
// ...

public void ConfigureServices(IServiceCollection services)
{
  // ...
  services.AddControllers(ClaimValueProviderFactory.AddInstance);
  // ...
}

// ...
```

## Usage
Here's an example of how you can get the email from the user claims:

```c#
public ActionResult GetMyEmail([FromClaim(ClaimTypes.Email)] string userEmail)
{
  return Ok(userEmail);
}
```
