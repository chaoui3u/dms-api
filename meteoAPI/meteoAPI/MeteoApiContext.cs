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
    }
}
