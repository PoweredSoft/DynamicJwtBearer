using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace PoweredSoft.DynamicJwtBearer.DynamicAuthority
{
    public interface IDynamicJwtBearerAuthorityResolver
    {
        public TimeSpan ExpirationOfConfiguration { get; }

        public Task<string> ResolveAuthority(HttpContext httpContext);
    }
}
