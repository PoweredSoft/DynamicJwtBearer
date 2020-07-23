using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using System;

namespace PoweredSoft.DynamicJwtBearer
{ 
    public static class DynamicJwtBearerExtensions
    {
        public static AuthenticationBuilder AddDynamicJwtBearer(this AuthenticationBuilder builder, string authenticationScheme, Action<JwtBearerOptions> action = null)
        {
            builder.Services.TryAddEnumerable(ServiceDescriptor.Singleton<IPostConfigureOptions<JwtBearerOptions>, JwtBearerPostConfigureOptions>());

            if (action != null)
                return builder.AddScheme<JwtBearerOptions, DynamicJwtBearerHandler>(authenticationScheme, null, action);

            return builder.AddScheme<JwtBearerOptions, DynamicJwtBearerHandler>(authenticationScheme, null, _ => { });
        }
    }
}
