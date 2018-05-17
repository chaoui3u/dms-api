using meteoAPI.Models;
using meteoAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OpenIddict.Core;
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
        private readonly IAuthorizationService _authzService;
      

        public AuthentificationController(
           IUserService userService,
           IAuthorizationService authzService)
        {
            _userService = userService;
            _authzService = authzService;
  
        }



        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet("users", Name = nameof(GetVisibleUsersAsync))]
        public async Task<IActionResult> GetVisibleUsersAsync(CancellationToken ct)
        {
            if (!ModelState.IsValid) return BadRequest(new ApiError(ModelState));

            
            IEnumerable<User> users = new List<User>();
            var mySelf = new User();
            // Authorization check. Is the user an admin?
            if (User.Identity.IsAuthenticated)
            {
                var canSeeEveryOne = await _authzService.AuthorizeAsync(User, "ViewAllUsersPolicy");
                if (canSeeEveryOne.Succeeded)
                {
                    users= await _userService.GetUsersAsync(ct);
                    var collectionLink = Link.ToCollection(nameof(GetVisibleUsersAsync));
                    var collection = new Collection<User>
                    {
                        Self = collectionLink,
                        Value = users.ToArray()
                    };
                    return Ok(collection);
                }
                else
                {
                     mySelf= await _userService.GetUserAsync(User);
                    return Ok(mySelf);
                }
            }

            return Unauthorized();
            

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

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPost("signup", Name = nameof(RegisterUserAsync))]
        public async Task<IActionResult> RegisterUserAsync(
            [FromBody] RegisterForm form,
            CancellationToken ct)
        {
            if (!ModelState.IsValid) return BadRequest(new ApiError(ModelState));

            //check if admin to add user
            if (User.Identity.IsAuthenticated)
            {
                var canSeeEveryOne = await _authzService.AuthorizeAsync(User, "ViewAllUsersPolicy");
                if (canSeeEveryOne.Succeeded)
                {
                    var (succeed, error) = await _userService.CreateUserAsync(form);
                    if (succeed) return Created(Url.Link(nameof(GetMeAsync), null), null);


                    return BadRequest(new ApiError
                    {
                        Message = "Registration failed",
                        Detail = error
                    });
                }
            }
            return Unauthorized();
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
