using meteoAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace meteoAPI.Services
{
    public interface ISiteService
    {
        Task<Site> GetSiteAsync(string id, CancellationToken ct);

        Task<IEnumerable<Site>> GetSitesAsync(CancellationToken ct);
    }
}
