using meteoAPI.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace meteoAPI
{
    public class MeteoApiContext :IdentityDbContext<UserEntity,UserRoleEntity, Guid>
    {
  
        public DbSet<WindEntity> Wind { get; set; }
        public DbSet<MainDataEntity> MainData { get; set; }
        public DbSet<WeatherRecordEntity> WeatherRecords { get; set; }

        public MeteoApiContext(DbContextOptions<MeteoApiContext> options):base(options)
        {
        }
        
      
    }
}
