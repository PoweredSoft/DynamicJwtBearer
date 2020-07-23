using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace PoweredSoft.DynamicJwtBearer.DynamicAuthority
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDynamicAuthorityJwtBearerResolver<TAuthoritySolver>(this IServiceCollection services)
            where TAuthoritySolver : class, IDynamicJwtBearerAuthorityResolver
        {
            services.AddTransient<IDynamicJwtBearerAuthorityResolver, TAuthoritySolver>();
            services.AddSingleton<IDynamicJwtBearerHanderConfigurationResolver, DynamicAuthorityJwtBearerHandlerConfigurationResolver>();
            return services;
        }

        public static AuthenticationBuilder AddDynamicAuthorityJwtBearerResolver<TAuthoritySolver>(this AuthenticationBuilder authenticationBuilder)
            where TAuthoritySolver : class, IDynamicJwtBearerAuthorityResolver
        {
            authenticationBuilder.Services.AddDynamicAuthorityJwtBearerResolver<TAuthoritySolver>();
            return authenticationBuilder;
        }
    }
}
