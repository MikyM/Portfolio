using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts;
using Entities.DataTransferObjects;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Domain.Helpers;
using System.Security.Claims;
using AuthService.Helpers;
using Domain.Services;

namespace Domain.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private readonly IUserService _userService;
        public UsersController(ILoggerManager logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        [HttpGet]
        public Task<IActionResult> GetAllUsers()
        {
            var usersResult = _userService.Get();
            return Task.FromResult<IActionResult>(Ok(usersResult));
        }

        /*[AllowAnonymous]
        // POST api/accounts
        [HttpPost("user/create")]
        public async Task<IActionResult> CreateUser([FromBody] AppUserForCreationDto user)
        {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            //var userIdentity = _mapper.Map<AppUser>(user);
            //var result = await _userManager.CreateAsync(userIdentity, user.Password);

            if (!result.Succeeded) return new BadRequestObjectResult(Errors.AddErrorsToModelState(result, ModelState));

            //await _appDbContext.JobSeekers.AddAsync(new JobSeeker { IdentityId = userIdentity.Id, Location = model.Location });
            //await _appDbContext.SaveChangesAsync();

            return Ok();
        }

        [AllowAnonymous]
        [HttpPut("user/update-information")]
        public async Task<IActionResult> UpdateUserInfo(Guid id, [FromBody] AppUserForUpdateDto user)
        {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

      
            var userIdentity = await _userManager.FindByIdAsync(id.ToString());
            _mapper.Map(user, userIdentity);
            userIdentity.Email = user.Email;
            userIdentity.UserName = user.UserName;
            userIdentity.FirstName = user.FirstName;
            userIdentity.LastName = user.LastName;
            var result = await _userManager.UpdateAsync(userIdentity);

            if (!result.Succeeded) return new BadRequestObjectResult(Errors.AddErrorsToModelState(result, ModelState));

            //await _appDbContext.JobSeekers.AddAsync(new JobSeeker { IdentityId = userIdentity.Id, Location = model.Location });
            //await _appDbContext.SaveChangesAsync();

            return NoContent();
        }

        [AllowAnonymous]
        [HttpPut("user/password-reset")]
        public async Task<IActionResult> UpdateUserPw(Guid id, string password)
        {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            var userIdentity = await _userManager.FindByIdAsync(id.ToString());
            var token = await _userManager.GeneratePasswordResetTokenAsync(userIdentity);
            var result = await _userManager.ResetPasswordAsync(userIdentity, token, password);

            if (!result.Succeeded) return new BadRequestObjectResult(Errors.AddErrorsToModelState(result, ModelState));

            //await _appDbContext.JobSeekers.AddAsync(new JobSeeker { IdentityId = userIdentity.Id, Location = model.Location });
            //await _appDbContext.SaveChangesAsync();

            return NoContent();
        }

        [Authorize(Policy = "Admin")]
        [HttpPut("user/create-admin")]
        public async Task<IActionResult> CreateAdminRole(Guid id)
        {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            var userIdentity = await _userManager.FindByIdAsync(id.ToString());
            if (userIdentity is null) return NotFound(id);
            var userClaims = await _userManager.GetClaimsAsync(userIdentity);
            if (userClaims.Any(x => x.Value.Equals("admin"))) return Ok(new { message = "User is admin already" });

            var result = await _userManager.AddClaimAsync(userIdentity, new Claim(Constants.Strings.JwtClaimIdentifiers.IsAdmin, Constants.Strings.JwtClaims.Admin));

            if (!result.Succeeded) return new BadRequestObjectResult(Errors.AddErrorsToModelState(result, ModelState));
            //await _appDbContext.JobSeekers.AddAsync(new JobSeeker { IdentityId = userIdentity.Id, Location = model.Location });
            //await _appDbContext.SaveChangesAsync();

            return NoContent();
        }*/
    }
}
