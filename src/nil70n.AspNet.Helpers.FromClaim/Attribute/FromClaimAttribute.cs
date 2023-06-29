using System;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace AspNet.Helpers.FromClaim
{
  [AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false)]
  public sealed class FromClaimAttribute : Attribute, IBindingSourceMetadata, IModelNameProvider
  {
    public FromClaimAttribute(string name)
    {
      Name = name;
    }

    public BindingSource BindingSource => ClaimBindingSource.Claim;

    public string Name { get; }
  }
}
