using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace AspNet.Helpers.FromClaim
{
  public static class ClaimBindingSource
  {
    public static readonly BindingSource Claim = new BindingSource(
      "Claim",
      "BindingSource_Claim",
      isGreedy: false,
      isFromRequest: true);
  }
}
