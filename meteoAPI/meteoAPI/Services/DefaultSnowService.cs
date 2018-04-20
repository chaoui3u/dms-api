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
    public class DefaultSnowService : ISnowService
    {
        private readonly MeteoApiContext _context;

        public DefaultSnowService(MeteoApiContext context)
        {
            _context = context;
        }

        public async Task<Snow> GetSnowAsync(int id, CancellationToken ct)
        {
            var entity = await _context.Snow.SingleOrDefaultAsync(s => s.Id == id, ct);
            if (entity == null) return null;

            return Mapper.Map<Snow>(entity);

        }

        public async Task<IEnumerable<Snow>> GetAllSnowAsync(CancellationToken ct)
        {
            var query = _context.Snow.ProjectTo<Snow>();

            return await query.ToArrayAsync();
        }
    }
}
