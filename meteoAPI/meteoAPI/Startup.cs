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
using Microsoft.AspNetCore.Identity;
using AspNet.Security.OpenIdConnect.Primitives;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

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
            services.AddDbContext<MeteoApiContext>(opt => 
                 {
                     //opt.UseInMemoryDatabase();
                     opt.UseSqlServer("Data Source=DESKTOP-BLRLJMH\\SQLEXPRESS;" +
                         "Initial Catalog=Weather;Integrated Security=True;" +
                         "Connect Timeout=30;Encrypt=False;" +
                         "TrustServerCertificate=False;" +
                         "ApplicationIntent=ReadWrite;" +
                         "MultiSubnetFailover=False");
                     opt.UseOpenIddict();
                 });

            // Map some of the default claim names to the proper OpenID Connect claim names
            services.Configure<IdentityOptions>(opt =>
            {
                opt.ClaimsIdentity.UserNameClaimType = OpenIdConnectConstants.Claims.Name;
                opt.ClaimsIdentity.UserIdClaimType = OpenIdConnectConstants.Claims.Subject;
                opt.ClaimsIdentity.RoleClaimType = OpenIdConnectConstants.Claims.Role;
            });

            // Add OpenIddict services
            services.AddOpenIddict<Guid>(opt =>
            {
                opt.AddEntityFrameworkCoreStores<MeteoApiContext>();
                opt.AddMvcBinders();

                opt.EnableTokenEndpoint("/token");
                opt.AllowPasswordFlow();
                services.AddAuthentication().AddOAuthValidation();
                services.AddSingleton<IAuthenticationSchemeProvider, CustomAuthenticationSchemeProvider>();
            

            });
            

            //Add ASP.NET core identity
            services.AddIdentity<UserEntity, UserRoleEntity>()
                .AddEntityFrameworkStores<MeteoApiContext>()
                .AddDefaultTokenProviders();

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
                opt.DefaultApiVersion = new ApiVersion(2, 0);
                opt.ApiVersionSelector = new CurrentImplementationApiVersionSelector(opt);
            });

            //services.Configure<Sites>(Configuration.GetSection("Sites"));


            services.AddScoped<IUserService, DefaultUserService>();
            services.AddScoped<IWeatherRecordService, DefaultWeatherRecordService>();
            services.AddScoped<IDateLogicService, DefaultWeatherRecordService>();
         
            //add Policy Authorisation
            services.AddAuthorization(opt=> 
            {
                opt.AddPolicy("ViewAllUsersPolicy",
                    p => p.RequireAuthenticatedUser().RequireRole("Admin"));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            //add test data in development
            //if (env.IsDevelopment())
            //{
            //    //Add test roles and users
            //    var roleManager = app.ApplicationServices
            //        .GetRequiredService<RoleManager<UserRoleEntity>>();
            //    var userManager = app.ApplicationServices
            //        .GetRequiredService<UserManager<UserEntity>>();



            //     AddTestUsers(roleManager, userManager, app.ApplicationServices.GetRequiredService<MeteoApiContext>()).Wait();
        //    AddTestData(app.ApplicationServices.GetRequiredService<MeteoApiContext>());
        //}

        app.UseHsts(opt => 
            {
                opt.MaxAge(days: 180);
                opt.IncludeSubdomains();
                opt.Preload();
            });
            
            app.UseAuthentication();
            
            app.UseMvc();
            
        }

        private static async Task AddTestUsers(RoleManager<UserRoleEntity> roleManager,
            UserManager<UserEntity> userManager, MeteoApiContext context)
        {
            //add a test role
            await roleManager.CreateAsync(new UserRoleEntity("Staff"));
            //add a test user
            var user = new UserEntity
            {
                Email = "admin@test.local",
                UserName = "admin@test.local",
                FirstName = "Admin",
                LastName = "Tester",
                CreatedAt = DateTimeOffset.UtcNow,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            context.Users.Add(user);

            await userManager.CreateAsync(user, "Password123!");
            context.SaveChanges();
            //Put the user in the Admin Role
            await userManager.AddToRoleAsync(user, "Admin");
            await userManager.UpdateAsync(user);
        }

        private static void AddTestData(MeteoApiContext context)
        {

            var MainDataVar = context.MainData.Add(new MainDataEntity
            {
                Temp = 30,
                Humidity = 10,
                Pressure = 995.93F,
            }).Entity;

           
            var WindVar = context.Wind.Add(new WindEntity
            {
                Speed = 120,
                Degree = 180
            }).Entity;
           

            context.WeatherRecords.Add(new WeatherRecordEntity
            {
                MainData = MainDataVar,
                Wind = WindVar,
                CurrentTime = DateTimeOffset.Now
            });
            context.SaveChanges();
        }
    }
}
