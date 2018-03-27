using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.Mvc;
using meteoAPI.Filters;

namespace meteoAPI
{
    public class Startup
    {
        private readonly int? _httpsPort;

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();

            //Get the https port only in development
            if(env.IsDevelopment())
            {
                var launchJsonConfig = new ConfigurationBuilder().
                    SetBasePath(env.ContentRootPath)
                    .AddJsonFile("Properties//launchSettings.json")
                    .Build();
                _httpsPort = launchJsonConfig.GetValue<int>("iisSettings:iisExpress:sslPort");
            }
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc(opt =>
            {
                opt.Filters.Add(typeof(JsonExceptionFilter));

                //Require Https for all controllers
                opt.SslPort = _httpsPort;
                opt.Filters.Add(typeof(RequireHttpsAttribute));
            });
            services.AddRouting(opt => opt.LowercaseUrls = true);
            services.AddApiVersioning(opt =>
            {
                opt.ApiVersionReader = new MediaTypeApiVersionReader();
                opt.AssumeDefaultVersionWhenUnspecified = true;
                opt.ReportApiVersions = true;
                opt.DefaultApiVersion = new ApiVersion(1, 0);
                opt.ApiVersionSelector = new CurrentImplementationApiVersionSelector(opt);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseHsts(opt => 
            {
                opt.MaxAge(days: 180);
                opt.IncludeSubdomains();
                opt.Preload();
            });

            app.UseMvc();
        }
    }
}
