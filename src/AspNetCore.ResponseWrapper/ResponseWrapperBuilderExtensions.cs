using System;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace AspNetCore.ResponseWrapper
{
    public static class ResponseWrapperBuilderExtensions
    {
        public static IMvcBuilder AddResponseWrapper(this IMvcBuilder mvcBuilder)
        {
            return AddResponseWrapper(mvcBuilder, _ => {});
        }

        public static IMvcBuilder AddResponseWrapper(this IMvcBuilder mvcBuilder, Action<ResponseWrapperOptions> action)
        {
            mvcBuilder.Services.Configure(action);
            mvcBuilder.ConfigureApiBehaviorOptions(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });
            mvcBuilder.Services.TryAddEnumerable(ServiceDescriptor.Transient<IApplicationModelProvider, ResponseWrapperApplicationModelProvider>());
            return mvcBuilder;
        }
    }
}