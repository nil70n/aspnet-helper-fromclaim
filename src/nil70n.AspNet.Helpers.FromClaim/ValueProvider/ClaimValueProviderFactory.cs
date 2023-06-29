using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace nil70n.AspNet.Helpers.FromClaim
{
  public sealed class ClaimValueProviderFactory : IValueProviderFactory
  {
    public Task CreateValueProviderAsync(ValueProviderFactoryContext context)
    {
      context.ValueProviders.Add(new ClaimValueProvider(
        ClaimBindingSource.Claim,
        context.ActionContext.HttpContext.User));

      return Task.CompletedTask;
    }
  }
}
