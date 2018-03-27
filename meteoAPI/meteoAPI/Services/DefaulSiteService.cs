using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using meteoAPI.Models;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace meteoAPI.Services
{
    public class DefaulSiteService : ISiteService
    {
        private readonly MeteoApiContext _context;

        public DefaulSiteService(MeteoApiContext context)
        {
            _context = context;
        }

        public async Task<Site> GetSiteAsync(string id, CancellationToken ct)
        {
            var entity = await _context.Sites.SingleOrDefaultAsync(s => s.Id == id, ct);
            if (entity == null) return null;

            return Mapper.Map<Site>(entity);
           
        }
    }
}
