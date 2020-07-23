# Allows to have a dynamic authority at runtime

With the help of changing the JwtHandler by a service, that allows you to define the openidconfiguration/multiple ones at runtime.

# Installing Nuget 

> Install nuget package to your awesome project.

| Full Version                 | NuGet                                                                                                                                                                                                                                                              |                                          NuGet Install |
| ---------------------------- | ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ | -----------------------------------------------------: |
| PoweredSoft.DynamicJwtBearer     | <a href="https://www.nuget.org/packages/PoweredSoft.DynamicJwtBearer/" target="_blank">[![NuGet](https://img.shields.io/nuget/v/PoweredSoft.DynamicJwtBearer.svg?style=flat-square&label=nuget)](https://www.nuget.org/packages/PoweredSoft.DynamicJwtBearer/)</a>             |     ```PM> Install-Package PoweredSoft.DynamicJwtBearer``` |
| PoweredSoft.DynamicJwtBearer.DynamicAuthority     | <a href="https://www.nuget.org/packages/PoweredSoft.DynamicJwtBearer.DynamicAuthority/" target="_blank">[![NuGet](https://img.shields.io/nuget/v/PoweredSoft.DynamicJwtBearer.DynamicAuthority.svg?style=flat-square&label=nuget)](https://www.nuget.org/packages/PoweredSoft.DynamicJwtBearer.DynamicAuthority/)</a>             |     ```PM> Install-Package PoweredSoft.DynamicJwtBearer.DynamicAuthority``` |

# How to use

> Startup class

```csharp
 public class Startup
    {
        private readonly IConfiguration configuration;

        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddMemoryCache();
            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddDynamicJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
                {
                    options.TokenValidationParameters.ValidateAudience = false;
                })
                .AddDynamicAuthorityJwtBearerResolver<ResolveAuthorityService>();

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
```

> Simple authority resolver with header X-Tenant

```csharp
internal class ResolveAuthorityService : IDynamicJwtBearerAuthorityResolver
    {
        private readonly IConfiguration configuration;

        public ResolveAuthorityService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public TimeSpan ExpirationOfConfiguration => TimeSpan.FromHours(1);

        public Task<string> ResolveAuthority(HttpContext httpContext)
        {
            var realm = httpContext.Request.Headers["X-Tenant"].FirstOrDefault() ?? configuration["KeyCloak:MasterRealm"];
            var authority = $"{configuration["KeyCloak:Endpoint"]}/realms/{realm}";
            return Task.FromResult(authority);
        }
    }
```
