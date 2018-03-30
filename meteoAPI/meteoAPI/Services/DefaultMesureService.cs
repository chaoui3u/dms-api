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
    public class DefaultMesureService : IMesureService
    {
        private readonly MeteoApiContext _context;

        public DefaultMesureService(MeteoApiContext context)
        {
            _context = context;
        }

        public async Task<Mesure> GetMesureAsync(string id, CancellationToken ct)
        {
            var entity = await _context.Mesures.SingleOrDefaultAsync(s => s.Id == id, ct);
            if (entity == null) return null;

            return Mapper.Map<Mesure>(entity);

        }

        public async Task<IEnumerable<Mesure>> GetMesuresAsync(CancellationToken ct)
        {
            var query = _context.Mesures.ProjectTo<Mesure>();

            return await query.ToArrayAsync();
        }
    }
}
