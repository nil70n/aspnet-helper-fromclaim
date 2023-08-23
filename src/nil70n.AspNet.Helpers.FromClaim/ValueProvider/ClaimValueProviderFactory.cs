using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace AspNet.Helpers.FromClaim
{
    public sealed class ClaimValueProviderFactory : IValueProviderFactory
    {
        public static Action<MvcOptions> AddInstance = (MvcOptions options) =>
            options.ValueProviderFactories.Add(new ClaimValueProviderFactory());

        public Task CreateValueProviderAsync(ValueProviderFactoryContext context)
        {
            context.ValueProviders.Add(new ClaimValueProvider(
              ClaimBindingSource.Claim,
              context.ActionContext.HttpContext.User));

            return Task.CompletedTask;
        }
    }
}
