
using meteoAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace meteoAPI.Services
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetUsersAsync(CancellationToken ct);
      
    }
}
