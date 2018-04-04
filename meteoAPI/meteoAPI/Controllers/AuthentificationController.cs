using meteoAPI.Models;
using meteoAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace meteoAPI.Controllers
{
    [Route("[controller]/")]
    public class AuthentificationController : Controller
    {
        private readonly IUserService _userService;

        public AuthentificationController(
           IUserService userService)
        {
            _userService = userService;
        }


        [HttpGet("logout",Name = nameof(GetLogOut))]
        public IActionResult GetLogOut()
        {
            throw new NotImplementedException();
        }

        [HttpGet("users/", Name = nameof(GetVisibleUsersAsync))]
        public async Task<IActionResult> GetVisibleUsersAsync(CancellationToken ct)
        {
            if (!ModelState.IsValid) return BadRequest(new ApiError(ModelState));

            // TODO: Authorization check. Is the user an admin?

            // TODO: Return a collection of visible users
            var users = await _userService.GetUsersAsync(ct);
            var collectionLink = Link.ToCollection(nameof(GetVisibleUsersAsync));
            var collection = new Collection<User>
            {
                Self = collectionLink,
                Value = users.ToArray()
            };
            return Ok(collection);
        }

        [Authorize]
        [HttpGet("{userId}", Name = nameof(GetUserByIdAsync))]
        public Task<IActionResult> GetUserByIdAsync(int userId, CancellationToken ct)
        {
            // TODO is userId the current user's ID?
            // If so, return myself.
            // If not, only Admin roles should be able to view arbitrary users.
            throw new NotImplementedException();
        }
    }
}
