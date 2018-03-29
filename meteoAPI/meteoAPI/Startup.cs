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
using meteoAPI.Models;
using Microsoft.EntityFrameworkCore;
using meteoAPI.Services;
using AutoMapper;
using meteoAPI.Infrastructure;
using Microsoft.AspNetCore.Mvc.Formatters;

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
            //using an in-memory database for dev
            //TODO:swap out with real database while in production
            services.AddDbContext<MeteoApiContext>(opt => opt.UseInMemoryDatabase());
            services.AddAutoMapper();
            // Add framework services.
            services.AddMvc(opt =>
            {
              
                opt.Filters.Add(typeof(JsonExceptionFilter));
                opt.Filters.Add(typeof(LinkRewritingFilter));
                //Require Https for all controllers
                opt.SslPort = _httpsPort;
                opt.Filters.Add(typeof(RequireHttpsAttribute));

                var jsonFormatter = opt.OutputFormatters.OfType<JsonOutputFormatter>().Single();
                opt.OutputFormatters.Remove(jsonFormatter);
                opt.OutputFormatters.Add(new IonOutputFormatter(jsonFormatter));
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

            //services.Configure<Sites>(Configuration.GetSection("Sites"));

            services.AddScoped<ISiteService, DefaultSiteService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            //add test data in development
            if (env.IsDevelopment())
            {
                var context = app.ApplicationServices.GetRequiredService<MeteoApiContext>();
                AddTestData(context);
            }

            app.UseHsts(opt => 
            {
                opt.MaxAge(days: 180);
                opt.IncludeSubdomains();
                opt.Preload();
            });

            app.UseMvc();
        }

        private static void AddTestData(MeteoApiContext context)
        {
            context.Sites.Add(new SiteEntity
            {
                Id = "CRIC",
                Refrence = "01505",
                Label = "CRIC Saint Quentin",
                Latitude = 0.87008541721366,
                Logitude = 0.05736315474888,
                Type = "0",
                Classification = null,
                Area = "Trafic"
            });

            context.Sites.Add(new SiteEntity
            {
                Id = "SM_SQ1",
                Refrence = "01506",
                Label = "P. Bert St Quentin",
                Latitude = 0.870274,
                Logitude = 0.05771077,
                Type = "0",
                Classification = "Périurbaine",
                Area = "De fond"
            });
            context.SaveChanges();
        }
    }
}
