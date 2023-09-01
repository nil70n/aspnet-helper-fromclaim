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

## Acknowledgments
Huge thanks for Dávid Kaya who made [this blog post](https://www.davidkaya.com/p/custom-from-attribute-for-controller-actions-in-asp-net-core). I'm using this solution for so long that I had a hard time finding the reference to include here.