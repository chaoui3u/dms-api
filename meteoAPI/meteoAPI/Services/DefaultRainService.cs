using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using meteoAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace meteoAPI.Services
{
    public class DefaultRainService :IRainService
    {
        private readonly MeteoApiContext _context;

        public DefaultRainService(MeteoApiContext context)
        {
            _context = context;
        }

        public async Task<Rain> GetRainAsync(Guid id, CancellationToken ct)
        {
            var entity = await _context.Rain.SingleOrDefaultAsync(s => s.Id == id, ct);
            if (entity == null) return null;

            return Mapper.Map<Rain>(entity);

        }

        public async Task<IEnumerable<Rain>> GetAllRainAsync(CancellationToken ct)
        {
            var query = _context.Rain.ProjectTo<Rain>();

            return await query.ToArrayAsync();
        }
    }
}
