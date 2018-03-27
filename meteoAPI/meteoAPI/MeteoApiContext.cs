using meteoAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace meteoAPI
{
    public class MeteoApiContext :DbContext
    {
        public MeteoApiContext(DbContextOptions options):base(options)
        {
        }

        public DbSet<SiteEntity> Sites { get; set; }
    }
}
