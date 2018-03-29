using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using meteoAPI.Models;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace meteoAPI.Services
{
    public class DefaultSiteService : ISiteService
    {
        private readonly MeteoApiContext _context;

        public DefaultSiteService(MeteoApiContext context)
        {
            _context = context;
        }

        public async Task<Site> GetSiteAsync(string id, CancellationToken ct)
        {
            var entity = await _context.Sites.SingleOrDefaultAsync(s => s.Id == id, ct);
            if (entity == null) return null;

            return Mapper.Map<Site>(entity);
           
        }

        public async Task<IEnumerable<Site>> GetSitesAsync(CancellationToken ct)
        {
            var query = _context.Sites.ProjectTo<Site>();

            return await query.ToArrayAsync();
        }
    }
}
