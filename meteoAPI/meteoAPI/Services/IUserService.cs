
using meteoAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace meteoAPI.Services
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetUsersAsync(CancellationToken ct);

        Task<(bool Succeed, string Error)> CreateUserAsync(RegisterForm form);
        Task<User> GetUserAsync(ClaimsPrincipal user);
    }
}
