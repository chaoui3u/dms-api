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
    
        public MeteoApiContext(DbContextOptions<MeteoApiContext> options):base(options)
        {
        }
        
        public DbSet<SiteEntity> Sites { get; set; }
        public DbSet<MesureEntity> Mesures { get; set; }
        public DbSet<CloudsEntity> Clouds { get; set; }
        public DbSet<WindEntity> Wind { get; set; }
        public DbSet<RainEntity> Rain { get; set; }
        public DbSet<SunEntity> Sun { get; set; }
        public DbSet<WeatherEntity> Weather { get; set; }
        public DbSet<WeatherHistoryEntity> WeatherHistory { get; set; }
        public DbSet<SnowEntity> Snow { get; set; }
        public DbSet<MainDataEntity> MainData { get; set; }
    }
}
