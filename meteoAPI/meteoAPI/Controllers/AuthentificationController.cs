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
    [Authorize(AuthenticationSchemes = "Bearer")]
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




        [HttpGet("users", Name = nameof(GetVisibleUsersAsync))]
        public async Task<IActionResult> GetVisibleUsersAsync(CancellationToken ct)
        {
            if (!ModelState.IsValid) return BadRequest(new ApiError(ModelState));

            
            IEnumerable<User> users = new List<User>();
            var mySelf = new User();
            // Authorization check. Is the user an admin?
            // only admin can see all users
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
                     mySelf= await _userService.GetMeAsync(User);
                    return Ok(mySelf);
                }
            }

            return Unauthorized();
            

        }


        [HttpGet("users/me", Name = nameof(GetMyUserAsync))]
        public async Task<IActionResult> GetMyUserAsync()
        {
            if (User == null) return BadRequest();

            var user = await _userService.GetMeAsync(User);

            return Ok(user);
        }


        [HttpPost("signup", Name = nameof(RegisterUserAsync))]
        public async Task<IActionResult> RegisterUserAsync(
            [FromBody] RegisterForm form)
        {
            if (!ModelState.IsValid) return BadRequest(new ApiError(ModelState));

            //check if admin to add user
            if (User.Identity.IsAuthenticated)
            {
                var canSeeEveryOne = await _authzService.AuthorizeAsync(User, "ViewAllUsersPolicy");
                if (canSeeEveryOne.Succeeded)
                {
                    var (succeed, error) = await _userService.CreateUserAsync(form);
                    if (succeed) return Created(Url.Link(nameof(GetMyUserAsync), null), null);


                    return BadRequest(new ApiError
                    {
                        Message = "Registration failed",
                        Detail = error
                    });
                }
            }
            return Unauthorized();
        }

        [HttpDelete("users/{userId}", Name = nameof(DeleteUserByIdAsync))]
        public async Task<ActionResult> DeleteUserByIdAsync(Guid userId)
        {
            if (User.Identity.IsAuthenticated)
            {
                //only admin can delete by id
                var canSeeEveryOne = await _authzService.AuthorizeAsync(User, "ViewAllUsersPolicy");
                if (canSeeEveryOne.Succeeded)
                {
                    //prohibiting user to delete himself
                    var myUser = await _userService.GetMyUserEntityAsync(User);
                    var thisUser = await _userService.GetUserEntityByIdAsync(userId);
                    if (thisUser == null) return NotFound();
                    if (myUser.Id == thisUser.Id) return Unauthorized();

                    //delete user
                    var result = await _userService.DeleteUserAsync(userId);
                    if (result) return Accepted();
                    else return NotFound();
                }
                else
                {
                    return Unauthorized();
                }
            }
            return Unauthorized();
        }
        [HttpPut("users/me", Name = nameof(ModifyMyUserAsync))]
        public async Task<IActionResult> ModifyMyUserAsync([FromBody] RegisterForm form)
        {
            //user can modify his own data
            //only admin can modify his role
            if (!ModelState.IsValid) return BadRequest(new ApiError(ModelState));
            if (User == null) return BadRequest();
            var user = await _userService.GetMyUserEntityAsync(User);
            var (succeed, error) = await _userService.ModifiyUserAsync(user.Id, form);
            if (succeed) return Accepted(Url.Link(nameof(GetMyUserAsync), null), null);

            return BadRequest(new ApiError
            {
                Message = "update failed",
                Detail = error
            });
        }

        [HttpPut("users/{userId}", Name = nameof(ModifyUserByIdAsync))]
        public async Task<ActionResult> ModifyUserByIdAsync(Guid userId, [FromBody] RegisterForm form)
        {
            if (!ModelState.IsValid) return BadRequest(new ApiError(ModelState));
            //only admin can modify user by id
            if (User.Identity.IsAuthenticated)
            {
                var canSeeEveryOne = await _authzService.AuthorizeAsync(User, "ViewAllUsersPolicy");
                if (canSeeEveryOne.Succeeded)
                {
                    var (succeed, error) = await _userService.ModifiyUserAsync(userId, form);
                    if (succeed) return Accepted(Url.Link(nameof(GetUserByIdAsync), null), null);

                    return BadRequest(new ApiError
                    {
                        Message = "update failed",
                        Detail = error
                    });
                }
                else
                {
                    return Unauthorized();
                }
            }
            return Unauthorized();
        }
        [HttpGet("users/{userId}", Name = nameof(GetUserByIdAsync))]
        public async Task<IActionResult> GetUserByIdAsync(Guid userId)
        {
            //only admin can get user by id
            if (User.Identity.IsAuthenticated)
            {
                var canSeeEveryOne = await _authzService.AuthorizeAsync(User, "ViewAllUsersPolicy");
                if (canSeeEveryOne.Succeeded)
                {
                    var user = await _userService.GetUserAsync(userId);
                    if (user == null) return NotFound();
                    return Ok(user);
                }
                else
                {
                    return Unauthorized();
                }
            }
            return Unauthorized();
        }
    }
}
