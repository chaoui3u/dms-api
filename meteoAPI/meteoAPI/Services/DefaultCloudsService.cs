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
    public class DefaultCloudsService : ICloudsService
    {
        private readonly MeteoApiContext _context;

        public DefaultCloudsService(MeteoApiContext context)
        {
            _context = context;
        }

        public async Task<Clouds> GetCloudsAsync(int id, CancellationToken ct)
        {
            var entity = await _context.Clouds.SingleOrDefaultAsync(s => s.Id == id, ct);
            if (entity == null) return null;

            return Mapper.Map<Clouds>(entity);

        }

        public async Task<IEnumerable<Clouds>> GetCloudsAsync(CancellationToken ct)
        {
            var query = _context.Clouds.ProjectTo<Clouds>();

            return await query.ToArrayAsync();
        }
    }
}
