
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
        Task<User> GetMeAsync(ClaimsPrincipal user);
        Task<bool> DeleteUserAsync(Guid userId);
        Task<(bool succeed, string error)> ModifiyUserAsync(Guid userId, RegisterForm form);
        Task<User> GetUserAsync(Guid userId);
        Task<UserEntity> GetMyUserEntityAsync(ClaimsPrincipal user);
        Task<UserEntity> GetUserEntityByIdAsync(Guid userId);
    }
}
