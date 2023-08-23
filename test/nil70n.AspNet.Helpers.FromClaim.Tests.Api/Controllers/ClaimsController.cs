
using System.Security.Claims;
using AspNet.Helpers.FromClaim;
using Microsoft.AspNetCore.Mvc;
using nil70n.AspNet.Helpers.FromClaim.Tests.Api.Models;

namespace nil70n.AspNet.Helpers.FromClaim.Tests.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClaimsController : ControllerBase
{
  [HttpGet("email")]
  public ActionResult<string> GetEmail([FromClaim(ClaimTypes.Email)] string email)
  {
    return Ok(email);
  }

  [HttpGet("name")]
  public ActionResult<string> GetName([FromClaim(ClaimTypes.Name)] string name)
  {
    return Ok(name);
  }

  [HttpGet("last_name")]
  public ActionResult<string> GetLastName([FromClaim("last_name")] string lastName)
  {
    return Ok(lastName);
  }

  [HttpGet("multiple")]
  public ActionResult<Person> GetMultiple(
      [FromClaim(ClaimTypes.Email)] string? email,
      [FromClaim(ClaimTypes.Name)] string? name,
      [FromClaim("last_name")] string? lastName)
  {
    return Ok(new Person(email, name, lastName));
  }
}
