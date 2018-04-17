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
    public class DefaultWeatherService : IWeatherService
    {
        private readonly MeteoApiContext _context;

        public DefaultWeatherService(MeteoApiContext context)
        {
            _context = context;
        }

        public async Task<Weather> GetWeatherAsync(int id, CancellationToken ct)
        {
            var entity = await _context.Weather.SingleOrDefaultAsync(s => s.Id == id, ct);
            if (entity == null) return null;

            return Mapper.Map<Weather>(entity);

        }

        public async Task<IEnumerable<Weather>> GetWeatherAsync(CancellationToken ct)
        {
            var query = _context.Weather.ProjectTo<Weather>();

            return await query.ToArrayAsync();
        }
    }
}
