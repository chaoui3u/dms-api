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
                Role = form.Role,
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
        public async Task<bool> DeleteUserAsync(Guid userId)
        {
            UserEntity user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null) return false;
             var result = await _userManager.DeleteAsync(user);
            return result.Succeeded;
        }

        public async Task<User> GetMeAsync(ClaimsPrincipal user)
        {
            var entity = await _userManager.GetUserAsync(user);
            return Mapper.Map<User>(entity);
        }
        public async Task<UserEntity> GetMyEntityAsync(ClaimsPrincipal user)
        {
            var entity = await _userManager.GetUserAsync(user);
            return entity;
        }

        public async Task<User> GetUserAsync(Guid userId)
        {
            var entity = await _userManager.FindByIdAsync(userId.ToString());
            if(entity == null) return null;
            return  Mapper.Map<User>(entity);
        }


        public async Task<(bool succeed, string error)> ModifiyUserAsync(Guid userId,RegisterForm form)
        {
            var user = _userManager.FindByIdAsync(userId.ToString()).Result;
            if(user == null)
            return (false, null);

            user.Email = form.Email;
            user.UserName = form.Email;
            user.FirstName = form.FirstName;
            user.LastName = form.LastName;
            user.PasswordHash = _userManager.PasswordHasher.HashPassword(user,form.Password);

            var roles = _userManager.GetRolesAsync(user);
            if ( roles.Result.FirstOrDefault() == "Admin")
            {
                user.Role = form.Role;
                await _userManager.AddToRoleAsync(user, form.Role);
            }

            var result= await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                var firstError = result.Errors.FirstOrDefault()?.Description;
                return (false, firstError);
            }

            return (true, null);
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
