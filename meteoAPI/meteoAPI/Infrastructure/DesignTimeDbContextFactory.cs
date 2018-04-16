using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace meteoAPI.Infrastructure
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<MeteoApiContext>
    {
        public MeteoApiContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            var builder = new DbContextOptionsBuilder<MeteoApiContext>();
            //var connectionString = configuration.GetConnectionString("Data Source=DESKTOP-BLRLJMH;" +
            //        "Initial Catalog=Weather;" +
            //        "Integrated Security=True;" +
            //        "Connect Timeout=30;Encrypt=False;" +
            //        "TrustServerCertificate=False;" +
            //        "ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            builder.UseSqlServer("Data Source=DESKTOP-BLRLJMH;" +
                    "Initial Catalog=Weather;" +
                    "Integrated Security=True;" +
                    "Connect Timeout=30;Encrypt=False;" +
                    "TrustServerCertificate=False;" +
                    "ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            return new MeteoApiContext(builder.Options);
        }
    }
}
