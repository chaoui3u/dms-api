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
    public class DefaultSunService : ISunService
    {
        private readonly MeteoApiContext _context;

        public DefaultSunService(MeteoApiContext context)
        {
            _context = context;
        }

        public async Task<Sun> GetSunAsync(int id, CancellationToken ct)
        {
            var entity = await _context.Sun.SingleOrDefaultAsync(s => s.Id == id, ct);
            if (entity == null) return null;

            return Mapper.Map<Sun>(entity);

        }

        public async Task<IEnumerable<Sun>> GetSunAsync(CancellationToken ct)
        {
            var query = _context.Sun.ProjectTo<Sun>();

            return await query.ToArrayAsync();
        }
    }
}
