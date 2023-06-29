using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Security.Claims;

namespace nil70n.AspNet.Helpers.FromClaim
{
  public sealed class ClaimValueProvider : BindingSourceValueProvider
  {
    private readonly ClaimsPrincipal _claimsPrincipal;

    public ClaimValueProvider(
      BindingSource bindingSource,
      ClaimsPrincipal claimsPrincipal) : base(bindingSource)
    {
      _claimsPrincipal = claimsPrincipal;
    }

    public override bool ContainsPrefix(string prefix)
        => _claimsPrincipal.HasClaim(claim => claim.Type == prefix);

    public override ValueProviderResult GetValue(string key)
    {
      var claimValue = _claimsPrincipal.FindFirstValue(key);
      return claimValue != null ? new ValueProviderResult(claimValue) : ValueProviderResult.None;
    }
  }
}
