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
    public class DefaultWindService : IWindService
    {
        private readonly MeteoApiContext _context;

        public DefaultWindService(MeteoApiContext context)
        {
            _context = context;
        }

        public async Task<Wind> GetWindAsync(int id, CancellationToken ct)
        {
            var entity = await _context.Wind.SingleOrDefaultAsync(s => s.Id == id, ct);
            if (entity == null) return null;

            return Mapper.Map<Wind>(entity);

        }

        public async Task<IEnumerable<Wind>> GetWindAsync(CancellationToken ct)
        {
            var query = _context.Wind.ProjectTo<Wind>();

            return await query.ToArrayAsync();
        }
    }
}
