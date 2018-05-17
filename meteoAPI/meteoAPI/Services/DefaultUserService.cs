using AutoMapper;
using AutoMapper.QueryableExtensions;
using meteoAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OpenIddict.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace meteoAPI.Services
{
    public class DefaultUserService : IUserService 
    {
        private readonly UserManager<UserEntity> _userManager;
        

        public DefaultUserService(UserManager<UserEntity> userManager,
            MeteoApiContext context)
        {
            _userManager = userManager;
       
        }
   

        public async Task<(bool Succeed, string Error)> CreateUserAsync(RegisterForm form)
        {
            var entity = new UserEntity
            {
                Email = form.Email,
                UserName = form.Email,
                FirstName= form.FirstName,
                LastName = form.LastName,
                CreatedAt = DateTimeOffset.UtcNow
            };
            var result = await _userManager.CreateAsync(entity, form.Password);
            await _userManager.AddToRoleAsync(entity, form.Role);
            await _userManager.UpdateAsync(entity);
            if (!result.Succeeded)
            {
                var firstError = result.Errors.FirstOrDefault()?.Description;
                return (false, firstError);
            }

            return (true, null);
        }

        public async Task<User> GetUserAsync(ClaimsPrincipal user)
        {
            var entity = await _userManager.GetUserAsync(user);
            return Mapper.Map<User>(entity);
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
