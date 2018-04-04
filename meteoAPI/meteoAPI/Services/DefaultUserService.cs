using AutoMapper.QueryableExtensions;
using meteoAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace meteoAPI.Services
{
    public class DefaultUserService : IUserService
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly MeteoApiContext _context;

        public DefaultUserService(UserManager<UserEntity> userManager, MeteoApiContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task<IEnumerable<User>> GetUsersAsync(CancellationToken ct)
        {
            IQueryable<UserEntity> query = _userManager.Users;

            var items = await query
                .ProjectTo<User>()
                .ToArrayAsync(ct);

            return items;
        }
    }
}
