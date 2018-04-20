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
    public class DefaultMainDataService : IMainDataService
    {
        private readonly MeteoApiContext _context;

        public DefaultMainDataService(MeteoApiContext context)
        {
            _context = context;
        }

        public async Task<MainData> GetMainDataAsync(Guid id, CancellationToken ct)
        {
            var entity = await _context.MainData.SingleOrDefaultAsync(s => s.Id == id, ct);
            if (entity == null) return null;

            return Mapper.Map<MainData>(entity);

        }

        public async Task<IEnumerable<MainData>> GetAllMainDataAsync(CancellationToken ct)
        {
            var query = _context.MainData.ProjectTo<MainData>();

            return await query.ToArrayAsync();
        }
    }
}
