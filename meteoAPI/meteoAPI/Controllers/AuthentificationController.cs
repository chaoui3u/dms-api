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

        [HttpGet("users", Name = nameof(GetVisibleUsersAsync))]
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

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet("users/me", Name = nameof(GetMeAsync))]
        public async Task<IActionResult> GetMeAsync(CancellationToken ct)
        {
            if (User == null) return BadRequest();

            var user = await _userService.GetUserAsync(User);
            if (user == null) return NotFound();

            return Ok(user);
        }

        [HttpPost("users", Name = nameof(RegisterUserAsync))]
        public async Task<IActionResult> RegisterUserAsync(
            [FromBody] RegisterForm form,
            CancellationToken ct)
        {
            if (!ModelState.IsValid) return BadRequest(new ApiError(ModelState));
            var (succeed, error) = await _userService.CreateUserAsync(form);
            if (succeed) return Created(Url.Link(nameof(GetMeAsync),null),null);
            return BadRequest(new ApiError
            {
                Message = "Registration failed",
                Detail = error
            });
        }

        [Authorize]
        [HttpGet("users/{userId}", Name = nameof(GetUserByIdAsync))]
        public Task<IActionResult> GetUserByIdAsync(Guid userId, CancellationToken ct)
        {
            // TODO is userId the current user's ID?
            // If so, return myself.
            // If not, only Admin roles should be able to view arbitrary users.
            throw new NotImplementedException();
        }
    }
}
